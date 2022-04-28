using Cognex.VisionPro;
using RemoteSupport;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SupportServer
{
    public class ServerTcp
    {
        public event EventHandler Listening;
        public void OnListening()
        {
            this.ListenStatus = Status.Listening;
            Listening?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler DisListened;
        public void OnDisListened()
        {
            this.ListenStatus = Status.DisListened;
            DisListened?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler Sending;
        public void OnSending()
        {
            this.SendStatus = Status.Sending;
            Sending?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler Sended;
        public void OnSended()
        {
            this.SendStatus = Status.Sended;
            Sended?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler Received;
        public event EventHandler Receiving;
        public void OnReceiving()
        {
            this.ReceiveStatus = Status.Receiving;
            Receiving?.Invoke(this, EventArgs.Empty);
        }
        public void OnReceived()
        {
            this.ReceiveStatus = Status.Received;
            Received?.Invoke(this, EventArgs.Empty);
        }
        public enum Status
        {
            None,
            Connecting,
            Listening,
            DisListened,
            Disconnecting,
            Running,
            Stoped,
            Sending,
            Sended,
            Receiving,
            Received,
            ReceiveSuccessfully,
            ReceiveFault,
            SendSuccessfully,
            SendFault,
            Free,
        }

        public Status SendStatus { get; set; }
        public Status RunStatus { get; set; }
        public Status ReceiveStatus { get; set; }
        public Status ListenStatus { get; set; }

        public int Timeout = 3000;
        public string ServerAddressString { get; set; }
        public IPAddress ServerIpAdress { get; set; }
        public int ServerPort { get; set; }
        public IPEndPoint ServerEp { get; set; }
        public Socket Client { get; set; }
        public bool SendCommand = false;
        public bool EnableRun = false;
        public byte[] SendData { get; set; }
        public byte[] RecieveData { get; set; }
        public Socket Listener { get; set; }
        public ServerTcp(string _serverAddressString, int _serverPort)
        {
            Init();
            this.ServerAddressString = _serverAddressString;
            this.ServerPort = _serverPort;
            this.ServerIpAdress = IPAddress.Parse(this.ServerAddressString);
            this.ServerEp = new IPEndPoint(this.ServerIpAdress, this.ServerPort);
            Thread _t = new Thread(InitListener);
            _t.Start();
            _t.Join();

            if (this.ListenStatus == Status.Listening)
            {
                Task _ = Run();
            }
        }
        public Dictionary<string, Task> ClientServiceList { get; set; }
        public Dictionary<string, Socket> ClientList { get; set; }

        public ServerTcp(IPEndPoint _iPEndPoint)
        {
            Init();
            this.ServerEp = _iPEndPoint;
        }

        public void InitListener()
        {
            this.EnableRun = true;
            this.RunStatus = Status.Running;
            int _trycount = 0;
            Thread _t = new Thread(() =>
            {
                while (_trycount < 5)
                {
                    try
                    {
                        this.Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        Listener.Bind(this.ServerEp);
                        break;
                    }
                    catch (Exception t)
                    {
                        _trycount++;
                    }
                }
            });
            _t.Start();
            _t.Join();
            if (_trycount == 5)
                throw new Exception("Can not binding to the Server Endpoint");
            OnListening();
            Listener.Listen(10);
        }

        public async Task Run()
        {
            Task _ = new Task(() =>
            {
                try
                {
                    Thread _t = new Thread(() =>
                    {
                        while (this.EnableRun)
                        {
                            Socket client = this.Listener.Accept();
                            string clientip = ((IPEndPoint)client.RemoteEndPoint).Address.ToString();
                            this.ClientList.Add(clientip, client);
                            this.ClientServiceList.Add(clientip,Service(clientip));
                        }
                    });
                    _t.Start();
                    while (this.EnableRun)
                    {
                        Thread.Sleep(10);
                    }
                }
                catch (Exception t)
                {

                }
            });
            _.Start();
            await _;
        }

        public async Task<CogImage8Grey> Service(string clientip)
        {
            CogImage8Grey _img = null;
            Task _ = new Task(() =>
            {
                try
                {
                    Socket Client = this.ClientList[clientip];
                    byte[] recieve = Receive(Client);
                    if (recieve != null)
                    {
                        Shipper _shipper = Shipper.ByteArrayToObject(recieve);
                        CogImage8Grey img = new CogImage8Grey(_shipper.Terminals["OutputImage"].Value as Bitmap);
                        _img = img;
                    }
                }
                catch (Exception t)
                {

                }
                finally
                {
                    
                }
            });
            _.Start();
            await _;
            Client.Close();
            return _img;
        }

        private void Init()
        {
            this.SendStatus = Status.Free;
            this.ReceiveStatus = Status.Free;
            this.ListenStatus = Status.DisListened;
            this.RunStatus = Status.Stoped;
            this.ClientServiceList = new Dictionary<string, Task>();
            this.ClientList = new Dictionary<string, Socket>();
        }

        public byte[] Receive(Socket Client)
        {
            this.OnReceiving();
            byte[] buffer = null;
            int _datalength;
            int _offset = 0;
            bool _condition = false;
            int _count = 0;
            int _timeout = this.Timeout / 10;
            Thread _ = new Thread(() =>
            {
                while (Client.Available == 0)
                {
                    Thread.Sleep(10);
                }
                byte[] byte_datalength = new byte[4];
                Client.Receive(byte_datalength);
                _datalength = BitConverter.ToInt32(byte_datalength, 0);
                buffer = new byte[_datalength];
                while (true)
                {
                    int _receive = Client.Receive(buffer, _offset, _datalength - _offset, SocketFlags.None);
                    _offset += _receive;
                    if (_offset >= _datalength) break;
                }
                _condition = true;
            });
            _.Start();
            while (!_condition && _count < _timeout)
            {
                Thread.Sleep(10);
                _count++;
            }
            if (_count == _timeout)
            {
                _.Abort();
                this.OnReceived();
                this.ReceiveStatus = Status.ReceiveFault;
                return null;
            }
            this.OnReceived();
            this.ReceiveStatus = Status.ReceiveSuccessfully;
            return buffer;
        }

        public bool Send(byte[] data)
        {
            this.OnSending();
            int _datalength = data.Length;
            int _offset = 0;
            bool _condition = false;
            int _count = 0;
            int _timeout = this.Timeout / 10;
            Thread _ = new Thread(() =>
            {
                this.Client.Send(BitConverter.GetBytes(_datalength));
                Thread.Sleep(10);
                while (true)
                {
                    int _sended = this.Client.Send(data, _offset, _datalength - _offset, SocketFlags.None);
                    _offset += _sended;
                    if (_offset == _datalength) break;
                }
                _condition = true;
            });
            _.Start();
            while (!_condition && _count < _timeout)
            {
                Thread.Sleep(10);
                _count++;
            }
            if (_count == _timeout)
            {
                _.Abort();
                this.OnSended();
                this.SendStatus = Status.SendFault;
                return false;
            }
            this.OnSended();
            this.SendStatus = Status.SendSuccessfully;
            return true;
        }
        public void SendToClient(byte[] data)
        {
            if (this.RunStatus != Status.Running || this.SendStatus == Status.Sending || this.ReceiveStatus == Status.Receiving)
                throw new Exception("Send condition is not meeted");
            this.SendData = data;
            this.SendCommand = true;
        }
    }
}
