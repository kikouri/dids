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
        private Hashtable _receivedAttacks;
        private UDPSecureSocket _socket;

        public MessageReceiverThread(Status status, Hashtable receivedAttacks, ArrayList statusMessages, KeysManager km)
        {
            _statusMessages = statusMessages;
            _receivedAttacks = receivedAttacks;
            _status = status;
            _socket = new UDPSecureSocket(2040, km);
            _status.Node.port = 2040;

            km.ReceiveSocket = _socket;

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
                    NewAttackMessage newAttack = (NewAttackMessage)receivedObject;
                    _receivedAttacks.Add(newAttack.AttackId, newAttack);
                }
            }
        }
    }
}
