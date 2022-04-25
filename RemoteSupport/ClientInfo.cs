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
    public class ClientInfo
    {
        public string Name { get; set; }
        public string[] CameraSerialNumber { get; set; }
        IPEndPoint ClientAddress { get; set; }

        public ClientInfo(string clientName, string[] cameraserialNumber)
        {
            if (string.IsNullOrEmpty(clientName)) throw new Exception("Client's Name must not null!");
            this.Name = Name;
            this.CameraSerialNumber = cameraserialNumber;
        }
        public ClientInfo(string clientName, string[] cameraserialNumber, IPEndPoint clientAddress)
        {
            if (string.IsNullOrEmpty(clientName)) throw new Exception("Client's Name must not null!");
            this.Name = Name;
            this.CameraSerialNumber = cameraserialNumber;
            this.ClientAddress = clientAddress;
        }

    }
}
