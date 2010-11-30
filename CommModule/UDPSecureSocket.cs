using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommModule
{
    class UDPSecureSocket
    {
        bool _bypass = true;
        UDPSocket _socket;

        public UDPSecureSocket(int port)
        {
            _socket = new UDPSocket(port);
        }

        public void sendMessage(Object message, String address)
        {
            if (_bypass)
                _socket.sendMessage(message, address);
            else
            {
            }
                    
        }

        public Object receiveMessage()
        {
            if (_bypass)
                return _socket.receiveMessage();
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
