using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Basler.Pylon;
using System.Threading;
using RemoteSupport;
using System.Net.NetworkInformation;

namespace Test_Client
{
    public partial class Client_FrmMain : Form
    {
        public IPAddress ServerAddress = IPAddress.Parse("127.0.0.1");
        public int ServerPort = 9999;
        public IPEndPoint ServerEp = null;
        public Socket TcpSender = null;

        private bool _enableRun = false;
        private bool _isRunning = false;
        private ICamera _mCamera = null;
        private bool _enableCapture = false;

        public void RaiseMessage(string message,bool clear = false)
        {
            this.tbxMessage.Invoke(new Action(() =>
            {
                if (clear)
                {
                    this.tbxMessage.Text = "";
                }
                this.tbxMessage.Text = this.tbxMessage.Text + DateTime.Now.ToString(">> hh::mm::ss >>") + message + Environment.NewLine;
            }));
        }

        public Client_FrmMain()
        {
            InitializeComponent();
            ServerEp = new IPEndPoint(ServerAddress, ServerPort);
            this.Shown += Client_FrmMain_Shown;
        }

        private void Client_FrmMain_Shown(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Display.Image = null;
        }

        public async Task Run()
        {
            this.RaiseMessage("Camera is Started!");
            this._isRunning = true;
            this._enableRun = true;
            Task _ = new Task(() =>
            {
                using(_mCamera = new Camera())
                {
                    try
                    {
                        _mCamera.Open();
                        _mCamera.Parameters[PLCamera.OffsetX].TrySetToMinimum();
                        _mCamera.Parameters[PLCamera.OffsetY].TrySetToMinimum();

                        _mCamera.Parameters[PLCamera.Width].SetValue(4608, IntegerValueCorrection.Nearest);
                        _mCamera.Parameters[PLCamera.Height].SetValue(3288, IntegerValueCorrection.Nearest);
                        if (_mCamera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.Mono8))
                        {
                            this.RaiseMessage("Set Image Format to MONO8 successfully!");
                        }
                        while (this._enableRun)
                        {
                            try
                            {

                                this.TcpSender = new Socket(SocketType.Stream, ProtocolType.Tcp);
                                if (!ConnectServer())
                                {
                                    throw new Exception("Pass Error!");
                                }
                                while (!this._enableCapture)
                                {
                                    Thread.Sleep(50);
                                    if (!this._enableRun) break;
                                }
                                this._mCamera.StreamGrabber.Start();
                                IGrabResult grabResult = this._mCamera.StreamGrabber.RetrieveResult(2000, TimeoutHandling.ThrowException);
                                using (grabResult)
                                {
                                    if (grabResult.GrabSucceeded)
                                    {
                                        this.RaiseMessage("Image grabbed successfully!");
                                        if (!this.TcpSender.Connected)
                                        {
                                            throw new Exception("TCPSender is not Connect!");
                                        }
                                        ImageShipper imageshipper = new ImageShipper(grabResult, "0x00", "12345",System.Drawing.Imaging.PixelFormat.Format8bppIndexed,PixelType.Mono8);
                                        this.Display.Image = imageshipper.BitmapImage;
                                        Ping mping = new Ping();
                                        PingReply reply = mping.Send(this.ServerAddress.ToString(), 1000);
                                        if(reply != null)
                                        {
                                            byte[] data = ImageShipper.ObjectToByteArray(imageshipper);
                                            this.RaiseMessage($"Data Length: {data.Length}");
                                            this.TcpSender.SendTimeout = -1;
                                            this.TcpSender.Send(data);
                                            this.RaiseMessage("Send Image Successfuly!");
                                            this.RaiseMessage("------------------------------------ <<");
                                        }
                                        else this.RaiseMessage("Send Image Timeout!");

                                    }
                                    else
                                    {
                                        this.RaiseMessage("Grabbed Image is Fault!");
                                    }
                                }
                            }
                            catch(Exception e)
                            {
                                this.RaiseMessage(e.Message);
                                break;
                            }
                            finally
                            {
                                this._mCamera.StreamGrabber.Stop();
                                this._enableCapture = false;
                                this.TcpSender.Close();
                            }
                        }
                        
                    }
                    catch(Exception e)
                    {
                        this.RaiseMessage(e.Message);
                    }
                    finally
                    {
                        if(_mCamera.IsOpen)
                        {
                            _mCamera.Close();
                        }
                    }
                }
            });
            _.Start();
            await _;
            this._isRunning = false;
            this.RaiseMessage("Running is Closed!");

        }
        public bool ConnectServer()
        {
            if (this.TcpSender.Connected)
            {
                this.RaiseMessage("ERROR! TCP is Connected!");
                return true;
            }
            int triercount = 0;
            while(triercount < 10)
            {
                try
                {
                    this.TcpSender.Connect(ServerEp);
                    this.RaiseMessage("TCP connect to Server Successfully!");
                    return true;
                }
                catch (Exception e)
                {
                    triercount++;
                    this.RaiseMessage(e.Message);
                    this.RaiseMessage("Connect Fault!");
                    this.RaiseMessage($"Reconnecting {triercount} ...");
                    Thread.Sleep(100);
                }
            }
            return false;
        }

        public bool DisconnectServer()
        {
            if (!this.TcpSender.Connected)
            {
                this.RaiseMessage("ERROR! TCP is not Connect!");
                return false;
            }
            this.TcpSender.Close();
            return true;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (!this._isRunning)
            {
                this.RaiseMessage("Trigger Command Fault >> Processing is stopping!");
                return;
            }
            if (this._enableCapture)
            {
                this.RaiseMessage("Trigger Command Fault >> Processing is on Processing!");
                return;
            }
            this._enableCapture = true;
        }

        private void btnOpenCam_Click(object sender, EventArgs e)
        {
            try
            {
                if(this._mCamera != null)
                {
                    if (this._isRunning)
                    {
                        Thread mt = new Thread(() => { CloseRunning(); });
                        mt.IsBackground = true;
                        mt.Start();
                        mt.Join();
                    }
                    if (this._mCamera.IsOpen)
                    {
                        this._mCamera.Close();
                    }
                    this.RaiseMessage("Camera is closed!");
                }
                Task _ = Run();
            }
            catch(Exception t)
            {
                this.RaiseMessage(t.Message);
            }
            
        }

        private void btnCloseCam_Click(object sender, EventArgs e)
        {
            if (this._isRunning)
            {
                Thread _ = new Thread(() => { CloseRunning(); });
                _.IsBackground = true;
                _.Start();
                _.Join();
            }
            if (this._mCamera.IsOpen)
            {
                this._mCamera.Close();
            }
            this.RaiseMessage("Camera is closed!");
        }

        private void btnRecon_Click(object sender, EventArgs e)
        {
            //if(this.DisconnectServer())
            //this.TcpSender = new Socket(SocketType.Stream, ProtocolType.Tcp);
            //this.ConnectServer();
        }

        private void CloseRunning()
        {
            if(this._mCamera == null) new Exception("Camera object is null!");
            int count = 0;
            this._enableRun = false;
            this._enableCapture = false;
            while (_isRunning && count < 100)
            {
                Thread.Sleep(10);
                count++;
            }
            if (count >= 100) new Exception("Closing Running is Timeout!");
        }
        private List<byte[]> CutData(byte[] data,int buffersize = 1024)
        {
            List<byte[]> Cutdata = new List<byte[]>();
            int count = data.Length / buffersize;
            int residual = data.Length % buffersize;
            for(int i = 0;i <= count; i++)
            {
                if (i < count)
                {
                    byte[] _ = new byte[count];
                    Array.Copy(data, i * buffersize, _, 0, buffersize);
                    Cutdata.Add(_);
                }
                else
                {
                    byte[] _ = new byte[count];
                    Array.Copy(data, i * buffersize, _, 0, residual);
                    Cutdata.Add(_);
                }
            }
            return Cutdata;
        } 
    }
}
