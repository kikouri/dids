using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using CommModule;
using CommModule.Messages;

namespace IDS
{
    class MessageReceiverThread
    {
        private Status _status;
        private ArrayList _statusMessages;
        private ArrayList _receivedAttacks;
        private UDPSecureSocket _socket;


        public MessageReceiverThread(Status status, ArrayList receivedAttacks, ArrayList statusMessages)
        {
            _statusMessages = statusMessages;
            _receivedAttacks = receivedAttacks;
            _status = status;
            _socket = new UDPSecureSocket(2040);
            _status.PortToReceive = 2040;
        }

        public void Run()
        {
            while (_status.IsOnline)
            {
                Object receivedObject = _socket.receiveMessage();
                String objectType = receivedObject.GetType().ToString();
                if (objectType == "CommModule.Messages.TrackerAnswerMessage")
                {
                    TrackerAnswerMessage trackerAnswer = (TrackerAnswerMessage)receivedObject;
                    _statusMessages.Add(trackerAnswer);
                }
                else if (objectType == "CommModule.Messages.NewAttackMessage")
                {
                }
            }
        }
    }
}
