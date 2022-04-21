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

namespace Test_Server
{
    public partial class FrmMain : Form
    {
        static public bool run = false;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            
        }

        public async Task Receive()
        {
            run = true;
            Task _ = new Task(() =>
            {

                try
                {
                    using (Socket Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    {
                        IPEndPoint ServerEP = new IPEndPoint(IPAddress.Parse("192.168.0.62"), 9999);
                        Listener.Bind(ServerEP);
                        Listener.Listen(10);
                        Socket client = Listener.Accept();
                        while (run)
                        {
                            int datalength = client.Available;
                            byte[] data = new byte[datalength];
                            int receivecount = client.Receive(data);
                            if (datalength != receivecount)
                            {
                                MessageBox.Show("Read Error!");
                            }
                            Bitmap image = new Bitmap(500, 500, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                            System.Drawing.Imaging.BitmapData imagedata = image.LockBits(new Rectangle(0, 0, 500, 500), System.Drawing.Imaging.ImageLockMode.WriteOnly, image.PixelFormat);
                            Marshal.Copy(data, 0, imagedata.Scan0, data.Length);
                            image.UnlockBits(imagedata);
                            Cognex.VisionPro.CogImage8Grey CogImage = new Cognex.VisionPro.CogImage8Grey(image);
                            this.cogDisplay.Invoke(new Action(() => { this.cogDisplay.Image = CogImage; }));
                        }
                    }
                }
                catch
                {

                }
            });
            _.Start();
            await _;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            run = false;
        }
    }
}
