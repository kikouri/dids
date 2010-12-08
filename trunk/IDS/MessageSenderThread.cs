using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using CommModule;

namespace IDS
{
    class MessageSenderThread
    {
        private Status _status;
        private ArrayList _messagesToSend;
        private UDPSecureSocket _socket;
        private ActiveNodes _activeNodes;

        public MessageSenderThread(Status status, ArrayList messagesToSend, ActiveNodes activeNodes)
        {
            _status = status;
            _messagesToSend = messagesToSend;
            _activeNodes = activeNodes;
            _socket = new UDPSecureSocket(2050);
        }

        public void Run()
        {
            while (_status.IsOnline)
            {
                if (_messagesToSend.Count != 0)
                {
                    Object messageToSend;

                    lock (_messagesToSend.SyncRoot)
                    {
                        messageToSend = _messagesToSend[0];
                        _messagesToSend.RemoveAt(0);
                    }

                    //send Message
                }
                else
                {
                    Thread.Sleep(20000);
                }


            }
        }
    }
}
