
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

using CommModule.Messages;

namespace CommModule
{
    public class UDPSocket
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

        //Sends an object, serializing it
        public void sendMessage(Object message, String address, int portToSend)
        {
            byte[] messageBytes = ObjectSerialization.SerializeGenericMessage(
                ObjectSerialization.SerializeObjectToGenericMessage(message));
            sendMessageBytes(messageBytes, address, portToSend);
        }

        //Sends a byte array
        public void sendMessageBytes(byte[] bytes, String address, int portToSend)
        {
            IPAddress ipAddress = IPAddress.Parse(address);
            IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, portToSend);
            _socket.SendTo(bytes, ipEndpoint);
        }

        //Returns a deserialized object
        public Object receiveMessage()
        {
            try
            {
                GenericMessage gm = receiveGenericMessage();
                
                Object message = ObjectSerialization.DeserializeGenericMessage(gm);                
                return message;
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("ObjectDisposedException "+e.ToString());
                return null;
            }
        }

        //Returns a GenericMessage
        public GenericMessage receiveGenericMessage()
        {
            try
            {
                IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                EndPoint endPoint = (EndPoint) remoteIpEndPoint;
                Byte[] messageBytes = receiveMessageBytes(ref endPoint);
                return ObjectSerialization.DeserializeObjectToGenericMessage(messageBytes);
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("ObjectDisposedException " + e.ToString());
                return null;
            }
        }

        //Returns a byte array
        public byte[] receiveMessageBytes(ref EndPoint remoteEndPoint)
        {
            byte[] messageBytes;
            int bytesReceived;

            try
            {
                messageBytes = new byte[_maxMessageSize];
                bytesReceived = _socket.ReceiveFrom(messageBytes, ref remoteEndPoint);
                byte[] ret = new byte[bytesReceived];
                Array.Copy(messageBytes, ret, bytesReceived);

                return ret;
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("ObjectDisposedException " + e.ToString());
                return null;
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException " + e.ToString());
                return null;
            }
        }

        public void close()
        {
            _socket.Close();
        }
    }
}