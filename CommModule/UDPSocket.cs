
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

        //Port 2020 to send, port 2021 to receive
        public UDPSocket(int port)
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint endPoint = new IPEndPoint(hostEntry.AddressList[0], 2020);

            _socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
                        
        }

        public void sendMessage(Object message, String address)
        {
            IPAddress ipAddress = IPAddress.Parse(address);
            IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, 2021);
            byte[] messageBytes = ObjectSerialization.SerializeObject(message);
            _socket.Send(messageBytes, messageBytes.Length, ipEndpoint);
        }

        public Object receiveMessage()
        {
            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 2020);
            try
            {
                Byte[] messageBytes = _socket.Receive(ref remoteIpEndPoint);
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