using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace RemoteSupport
{
    public class ClientBase
    {
        public enum DeleveryStatus
        {
            Free,
            Sending,
            Sended,
            Receiving,
            Received,
        }

        public enum DeleveryResult
        {
            Free,
            Success,
            Fault
        }

        public DeleveryStatus SendStatus { get; set; } = DeleveryStatus.Free;
        public DeleveryStatus ReceiveStatus { get; set; } = DeleveryStatus.Free;
        public DeleveryResult SendResult { get; set; } = DeleveryResult.Free;
        public DeleveryResult ReceiveResult { get; set; } = DeleveryResult.Free;

        public event EventHandler Sending;
        public event EventHandler Sended;
        public event EventHandler Receiving;
        public event EventHandler Received;

        public Socket TcpSocket{get;set;}

        public ClientBase()
        {

        }

        public ClientBase(Socket _tcpSocket)
        {
            this.TcpSocket = _tcpSocket;
        }

        public void OnSending()
        {
            Sending?.Invoke(this, EventArgs.Empty);
            SendStatus = DeleveryStatus.Sending;
        }

        public void OnSended()
        {
            Sended?.Invoke(this, EventArgs.Empty);
            SendStatus = DeleveryStatus.Sended;
        }

        public void OnReceiving()
        {
            Receiving?.Invoke(this, EventArgs.Empty);
            ReceiveStatus = DeleveryStatus.Receiving;
        }

        public void OnReceived()
        {
            Received?.Invoke(this, EventArgs.Empty);
            ReceiveStatus = DeleveryStatus.Received;
        }
    }
}
