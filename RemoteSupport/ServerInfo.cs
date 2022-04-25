using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace RemoteSupport
{
    [Serializable()]
    public class ServerInfo
    {
        public string Name { get; set; }
        public IPEndPoint ServerEnpoint { get; set; }

        public ServerInfo(string _name)
        {
            this.Name = _name;
        }

        public ServerInfo(string _name, IPEndPoint _serverEnpoint)
        {
            this.Name = Name;
            this.ServerEnpoint = _serverEnpoint;
        }
    }
}
