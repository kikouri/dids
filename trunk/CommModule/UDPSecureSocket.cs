using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using System.Security.Cryptography;

namespace CommModule
{
    public class UDPSecureSocket
    {
        bool _bypass = true;
        UDPSocket _socket;
        private int receivePort;

        public UDPSecureSocket(int port)
        {
            _socket = new UDPSocket(port);
            receivePort = port;

        }

        public void sendMessage(Object message, String address, int portToSend)
        {
            if (_bypass)
                _socket.sendMessage(message, address, portToSend);
            else
            {
            }
                    
        }

        public Object receiveMessage()
        {
            if (_bypass)
            {
                return _socket.receiveMessage();
            }
            else
            {
                return null;
            }

        }

        public void close()
        {
            _socket.close();
        }
    }
}
