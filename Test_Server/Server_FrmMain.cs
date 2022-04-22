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

namespace Test_Server
{
    public partial class Server_FrmMain : Form
    {
        static public bool run = false;
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
                            while (datalength == 0)
                            {
                                Thread.Sleep(10);
                                datalength = client.Available;
                            }
                            this.RaiseMessage($"Data available: {datalength}");
                            byte[] data = new byte[datalength];
                            int receivecount = client.Receive(data);
                            this.RaiseMessage($"Data Get: {receivecount}");
                            if (datalength != receivecount)
                            {
                                this.RaiseMessage("Read Error!");
                            }
                            ImageShipper imageShipper = ImageShipper.ByteArrayToObject(data) as ImageShipper;
                            CogImage8Grey image = new CogImage8Grey(imageShipper.BitmapImage);
                            this.cogDisplay.Invoke(new Action(() => { this.cogDisplay.Image = image; }));
                            this.RaiseMessage("OK!");
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
            this.cogDisplay = null;
        }
    }
}
