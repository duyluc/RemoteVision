using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SupportServer;

namespace Test_ServerV2
{
    public partial class ServerMain : Form
    {
        public SupportServer.ServerTcp ServerTcp { get; set; }
        public ServerMain()
        {
            InitializeComponent();
            string ip = this.tbServerAddress.Text.Split(':')[0];
            int port = int.Parse(this.tbServerAddress.Text.Split(':')[1]);
            this.ServerTcp = new ServerTcp(ip, port);
        }
    }
}
