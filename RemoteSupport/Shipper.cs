using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Basler.Pylon;

namespace RemoteSupport
{
    [Serializable()]
    public class Shipper
    {
        public Dictionary<string, Terminal> Terminals { get; set; }
        public ClientInfo ClientInfor { get; set; }
        public ServerInfo ServerInfor { get; set; }
        public Shipper()
        {
            //Nothing
        }

        public Shipper(ServerInfo _serverInfo)
        {
            this.Terminals = new Dictionary<string, Terminal>();
            this.ServerInfor = _serverInfo;
        }

        public Shipper(Dictionary<string,Terminal> terminals)
        {
            this.Terminals = new Dictionary<string, Terminal>();
            this.Terminals = terminals;
        }

        public void AddTerminal(Terminal _terminal)
        {
            this.Terminals = new Dictionary<string, Terminal>();
            Terminals.Add(_terminal.Name, _terminal);
        }

        static public byte[] ObjectToByteArray(Shipper obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        static public Shipper ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Shipper obj = binForm.Deserialize(memStream) as Shipper;
            return obj;
        }

    }
}
