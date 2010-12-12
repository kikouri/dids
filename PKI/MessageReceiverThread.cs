using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using CommModule;
using CommModule.Messages;

namespace PKI
{
    /*
     * Receives and buffers all messages destined to the PKI.
     */
    class MessageReceiverThread
    {
        private SyncBuffer _buffer;

        private UDPSecureSocket _socket;


        public MessageReceiverThread(SyncBuffer buf, int portToListen)
        {
            _buffer = buf;
            _socket = new UDPSecureSocket(portToListen);
        }

        public void Run()
        {
            while (true)
            {
                Object receivedObject = _socket.receiveMessage();

                _buffer.insert(receivedObject);
            }
        }
    }
}
