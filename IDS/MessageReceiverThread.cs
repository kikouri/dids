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
        private ArrayList _receivedSolutions;
        private UDPSecureSocket _socket;

        public MessageReceiverThread(Status status, Hashtable receivedAttacks, ArrayList statusMessages,ArrayList receivedSolutions, KeysManager km, int port)
        {
            _statusMessages = statusMessages;
            _receivedAttacks = receivedAttacks;
            _status = status;
            _receivedSolutions = receivedSolutions;
            _socket = new UDPSecureSocket(port, km);
            _status.Node.port = port;

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
                    lock (_statusMessages.SyncRoot)
                    {
                        _statusMessages.Add(trackerAnswer);
                    }
                }
                else if (objectType == "CommModule.Messages.NewAttackMessage")
                {
                    NewAttackMessage newAttack = (NewAttackMessage)receivedObject;
                    lock (_receivedAttacks.SyncRoot)
                    {
                        _receivedAttacks.Add(newAttack.AttackId, newAttack);
                    }
                }
                else if (objectType == "CommModule.Messages.AttackSolutionMessage")
                {
                    AttackSolutionMessage attackSolution = (AttackSolutionMessage)receivedObject;
                    lock (_receivedSolutions.SyncRoot)
                    {
                        _receivedSolutions.Add(attackSolution);
                    }
                }
            }
        }
    }
}
