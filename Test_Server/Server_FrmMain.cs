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
using System.Runtime.InteropServices;
using RemoteSupport;
using Cognex.VisionPro;
using System.Drawing.Imaging;
using Basler.Pylon;
using System.Threading;
using System.IO;

namespace Test_Server
{
    public partial class Server_FrmMain : Form
    {
        static public bool run = false;
        static public bool enableWaitdata = false;
        static private int Buffersize = 9216;
        public Server_FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            Task _ = Receive();
        }

        public void RaiseMessage(string message, bool clear = false)
        {
            this.tbxMessage.Invoke(new Action(() =>
            {
                if (clear)
                {
                    this.tbxMessage.Text = "";
                }
                this.tbxMessage.Text = this.tbxMessage.Text + DateTime.Now.ToString(">> hh::mm::ss >>") + message + Environment.NewLine;
                this.tbxMessage.SelectionStart = this.tbxMessage.Text.Length;
                this.tbxMessage.ScrollToCaret();
            }));
        }

        public async Task Receive()
        {
            run = true;
            Task _ = new Task(() =>
            {
                using (Socket Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IPEndPoint ServerEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
                    Listener.Bind(ServerEP);
                    Listener.Listen(10);
                    Socket client = null;
                    while (run)
                    {
                        try
                        {
                            this.RaiseMessage($"Listening...");
                            client = Listener.Accept();
                            this.RaiseMessage($"Connect to TCP!");
                            this.RaiseMessage($"Waiting for Data!");
                            int datalength = client.Available;
                            enableWaitdata = true;
                            while (datalength == 0)
                            {
                                Thread.Sleep(10);
                                datalength = client.Available;
                                if (!enableWaitdata) throw new Exception("Data Waiting is Aborted");
                            }
                            byte[] _data = new byte[datalength];
                            client.Receive(_data);
                            datalength = BitConverter.ToInt32(_data, 0);
                            byte[] assamplyData = new byte[datalength];
                            int _countpocket = datalength / Buffersize;
                            int _residual = datalength % Buffersize;
                            _data = new byte[Buffersize];
                            this.RaiseMessage($"Data available: {datalength}");
                            bool hasdata = true;
                            int offset = 0;
                            _data = new byte[datalength];
                            while (hasdata)
                            {

                                bool _c = false;
                                Thread _t = new Thread(() =>
                                {

                                    int read = client.Receive(_data, offset, _data.Length - offset, SocketFlags.None);
                                    offset += read;
                                    _c = true;
                                    
                                });
                                int _count = 0;
                                _t.Start();
                                while (!_c && _count<50)
                                {
                                    Thread.Sleep(10);
                                    _count++;
                                }
                                if (_count < 50)
                                {
                                    if (offset == _data.Length) break;
                                }
                                else
                                {
                                    if(_t.IsAlive) _t.Abort();
                                    throw new Exception("Receive Timeout!");
                                }
                            }
                            if (datalength != assamplyData.Length)
                            {
                                this.RaiseMessage("Read Error!");
                            }
                            this.RaiseMessage($"Received: {offset} byte");
                            Shipper imageShipper = Shipper.ByteArrayToObject(_data) as Shipper;
                            //CogImage8Grey image = new CogImage8Grey(imageShipper.BitmapImage);
                            //this.cogDisplay.Invoke(new Action(() => { this.cogDisplay.Image = image; }));
                            //this.RaiseMessage($"Device: {imageShipper.UnitID}" + Environment.NewLine + $"Camera: {imageShipper.CameraSerialNumber}");
                            this.RaiseMessage("Receive Image Successfully!");
                        }
                        catch(Exception e)
                        {
                            this.RaiseMessage(e.Message);
                        }
                        finally
                        {
                            client.Close();
                        }
                    }
                }
            });
            _.Start();
            await _;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.cogDisplay.Image = null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            enableWaitdata = false;
        }
    }
}
