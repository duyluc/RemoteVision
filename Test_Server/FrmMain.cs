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
        public FrmMain()
        {
            InitializeComponent();
            using (Socket Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IPEndPoint ServerEP = new IPEndPoint(IPAddress.Parse("192.168.0.62"), 9999);
                Listener.Bind(ServerEP);
                Socket client = Listener.Accept();
                int datalength = client.Available;
                byte[] data = new byte[datalength];
                int receivecount = client.Receive(data);
                if(datalength != receivecount)
                {
                    MessageBox.Show("Read Error!");
                }
                Bitmap image = new Bitmap(500, 500, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                System.Drawing.Imaging.BitmapData imagedata = image.LockBits(new Rectangle(0, 0, 500, 500), System.Drawing.Imaging.ImageLockMode.WriteOnly, image.PixelFormat);
                Marshal.Copy(data, 0, imagedata.Scan0, data.Length);
                image.UnlockBits(imagedata);
                Cognex.VisionPro.CogImage8Grey CogImage = new Cognex.VisionPro.CogImage8Grey(image);
                this.cogDisplay.Image = CogImage;
                MessageBox.Show("received!");
            }
        }
    }
}
