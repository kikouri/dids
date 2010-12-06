
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace CommModule
{
    class UDPSocket
    {
        private Socket _socket;
        private int _maxMessageSize = 524288;

        //Port 2020 to send, port 2021 to receive
        public UDPSocket(int port)
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, port);
            EndPoint endPoint = (EndPoint)ipEndPoint;

            _socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            _socket.Bind(endPoint);
                        
        }

        public void sendMessage(Object message, String address, int portToSend)
        {
            IPAddress ipAddress = IPAddress.Parse(address);
            IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, portToSend);
            byte[] messageBytes = new byte[_maxMessageSize];
            messageBytes = ObjectSerialization.SerializeObject(message);
            _socket.SendTo(messageBytes, ipEndpoint);
        }

        public Object receiveMessage()
        {
            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any,0);
            EndPoint remoteEndPoint = (EndPoint)remoteIpEndPoint;
            try
            {
                Byte[] messageBytes = new byte[_maxMessageSize];
                _socket.ReceiveFrom(messageBytes,ref remoteEndPoint);
                Object message = ObjectSerialization.DeserializeObject(messageBytes);                
                return message;
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("ObjectDisposedException "+e.ToString());
                return null;
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException "+e.ToString());
                return null;
            }
        }

        public void close()
        {
            _socket.Close();
        }
    }
}