using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SupportServer
{
    public class ClientTcp
    {
        public event EventHandler Connected;
        public void OnConnected()
        {
            this.ConnectStatus = Status.Connected;
            Connected?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler Disconnected;
        public void OnDisconnected()
        {
            this.ConnectStatus = Status.Disconnected;
            Disconnected?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler Connecting;
        public void OnConnecting()
        {
            this.ConnectStatus = Status.Connecting;
            Connecting?.Invoke(this, EventArgs.Empty);
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
            Connected,
            Disconnected,
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
        public Status ConnectStatus { get; set; }

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
        public ClientTcp()
        {
            Init();
        }
        public ClientTcp(string _serverAddressString, int _serverPort)
        {
            Init();
            this.ServerAddressString = _serverAddressString;
            this.ServerPort = _serverPort;
            this.ServerIpAdress = IPAddress.Parse(this.ServerAddressString);
            this.ServerEp = new IPEndPoint(this.ServerIpAdress, this.ServerPort);
        }

        public ClientTcp(IPEndPoint _iPEndPoint)
        {
            Init();
            this.ServerEp = _iPEndPoint;
        }

        private void Init()
        {
            this.SendStatus = Status.Free;
            this.ReceiveStatus = Status.Free;
            this.ConnectStatus = Status.Disconnected;
            this.RunStatus = Status.Stoped;
        }

        public void Connect()
        {
            if (this.ServerEp == null)
            {
                throw new Exception("Server Enpoint is not initialized");
            }
            this.Client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            if (this.ConnectStatus == Status.Connected||this.Client.Connected) throw new Exception("Client and Server is connected");
            this.Client.Connect(this.ServerEp);
            Task _ = this.Run();
            this.OnConnected();
        }

        public void Disconnect()
        {
            if (this.ConnectStatus == Status.Disconnected || !this.Client.Connected) throw new Exception("Client and Server is disconnected");
            this.Client.Close();
            this.EnableRun = false;
            this.OnDisconnected();
        }

        public bool tryReconnect(int iter = 10)
        {
            if (!this.Client.Connected)
            {
                this.ConnectStatus = Status.Disconnected;
            }
            for(int i = 0; i < iter; i++)
            {
                this.Connect();
                if (this.Client.Connected) return true;
            }
            return false;
        }

        public async Task Run()
        {
            try
            {
                this.EnableRun = true;
                this.RunStatus = Status.Running;
                this.SendCommand = false;
                Task _ = new Task(() =>
                {
                    while (this.EnableRun)
                    {
                        //Waiting SendData and Send command
                        while (this.EnableRun && !this.SendCommand)
                        {
                            Thread.Sleep(10);
                        }
                        this.SendCommand = false;
                        if (!this.Client.Connected)
                        {
                            if (!this.tryReconnect()) throw new Exception("Can not connect to Server");
                        }

                        int _countTryToSend =0 ;
                        //Send Data
                        try
                        {
                            if (!this.Send(this.SendData)) throw new Exception("Send Timeout");
                        }
                        catch (Exception t)
                        {
                            if(_countTryToSend == 0&& this.SendStatus == Status.Sending)
                            {
                                this.Send(this.SendData);
                                _countTryToSend++;
                            }
                            else
                            {
                                throw t;
                            }
                        }
                        //Receive Data
                        try
                        {
                            if(!Receive()) throw new Exception("Receive Timeout");
                        }
                        catch(Exception t)
                        {

                        }
                    }
                });
                _.Start();
                await _;
            }
            catch (Exception t)
            {

            }
            finally
            {
                this.RunStatus = Status.Running;
            }
        }

        public bool Receive()
        {
            this.OnReceiving();
            byte[] buffer;
            int _datalength;
            int _offset = 0;
            bool _condition = false;
            int _count = 0;
            int _timeout = this.Timeout / 10;
            Thread _ = new Thread(() =>
            {
                while (this.Client.Available == 0)
                {
                    Thread.Sleep(10);
                }
                byte[] byte_datalength = new byte[4];
                this.Client.Receive(byte_datalength);
                _datalength = BitConverter.ToInt32(byte_datalength,0);
                buffer = new byte[_datalength];
                while (true)
                {
                    int _receive = this.Client.Receive(buffer, _offset, _datalength - _offset, SocketFlags.None);
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
            if(_count == _timeout)
            {
                _.Abort();
                this.OnReceived();
                this.ReceiveStatus = Status.ReceiveFault;
                return false;
            }
            this.OnReceived();
            this.ReceiveStatus = Status.ReceiveSuccessfully;
            return true;
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
            if(_count == _timeout)
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
        public void SendToServer(byte[] data)
        {
            if (this.RunStatus != Status.Running || this.SendStatus == Status.Sending || this.ReceiveStatus == Status.Receiving) 
                throw new Exception("Send condition is not meeted");
            this.SendData = data;
            this.SendCommand = true;
        }
    }
}
