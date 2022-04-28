using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RemoteSupport
{
    public class Delevery
    {
        public IPEndPoint TcpIPEndpoint { get; set; }
        public int SendTimeout { get; set; } = 3000; //3000 ms
        public int ReceivetimeOut { get; set; } = 3000; //3000 ms
        private int WaitingGetSampleTime = 10; //10 ms
        private Exception ConnectInterruptEx { get; set; } = new Exception("0x00-->Connect is Interrupted!");

        public Delevery(string _iPAddress, int _port)
        {
            IPAddress _address = IPAddress.Parse(_iPAddress);
            this.TcpIPEndpoint = new IPEndPoint(_address, _port);

        }

        public byte[] Recieve(Socket _tcpSocket)
        {
            byte[] _recieveData = null;
            try
            {
                // Temporary variation for handler Receive status
                bool _receivecomplite = false;
                //bool _istimeout = false;
                int _timeout = this.ReceivetimeOut / this.WaitingGetSampleTime;
                int _waitingcounter = 0;
                byte[] byte_receivedatalength = new byte[4];
                int _receivedatalength = 0;
                int offset = 0;
                if (!_tcpSocket.Connected) throw this.ConnectInterruptEx;

                Thread _t = new Thread(()=>
                {
                    while(_tcpSocket.Available == 0)
                    {
                        Thread.Sleep(this.WaitingGetSampleTime);
                    }
                    int read = _tcpSocket.Receive(byte_receivedatalength, 0, 4, SocketFlags.None);
                    if(read < 4)
                    {
                        throw new Exception("0x02-->Memo Data length is invalid!");
                    }
                    _receivedatalength = BitConverter.ToInt32(byte_receivedatalength,0);
                    _recieveData = new byte[_receivedatalength];
                    while (true)
                    {
                        read = _tcpSocket.Receive(_recieveData, offset, _receivedatalength - offset, SocketFlags.None);
                        offset += read;
                        if (offset == _receivedatalength) break;
                    }
                    _receivecomplite = true;
                });
                _t.Start();
                while (!_receivecomplite && _waitingcounter < _timeout)
                {
                    Thread.Sleep(this.WaitingGetSampleTime);
                    _waitingcounter++;
                }
                if (!_receivecomplite)
                {
                    if (_t.IsAlive) _t.Abort();
                    throw new Exception("0x01-->Receive Timeout!");
                }
            }
            catch (Exception t)
            {
                _recieveData = null;
                throw t;
            }
            finally
            {

            }
            return _recieveData;
        }

        public bool Send(Socket _tcpSocket, byte[] _sendData)
        {
            bool _sendcomplite = false;
            try
            {
                int _datalength = _sendData.Length;
                //bool _istimeout = false;
                int _timeout = this.ReceivetimeOut / this.WaitingGetSampleTime;
                int _waitingcounter = 0;
                byte[] byte_receivedatalength = BitConverter.GetBytes(_datalength);
                int offset = 0;
                if(!_tcpSocket.Connected) throw this.ConnectInterruptEx;

                Thread _t = new Thread(() =>
                {
                    while (true)
                    {
                        int write = _tcpSocket.Send(byte_receivedatalength, offset, 4 - offset, SocketFlags.None);
                        offset += write;
                        if (offset == 4) break;
                    }
                    Thread.Sleep(20);
                    offset = 0;
                    while (true)
                    {
                        int write = _tcpSocket.Send(_sendData, offset, _datalength - offset, SocketFlags.None);
                        offset += write;
                        if (offset == _datalength) break;
                    }
                    _sendcomplite = true;
                });

                _t.Start();
                while (!_sendcomplite && _waitingcounter < _timeout)
                {
                    Thread.Sleep(this.WaitingGetSampleTime);
                    _waitingcounter++;
                }
                if (!_sendcomplite)
                {
                    if (_t.IsAlive) _t.Abort();
                    throw new Exception("0x03-->Send Timeout!");
                }

            }
            catch(Exception t)
            {
                _sendcomplite = false;
                throw t;
            }
            finally
            {

            }
            return _sendcomplite;
        }
    }
}
