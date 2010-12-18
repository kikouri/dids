using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using CommModule;
using CommModule.Messages;
using System.Windows.Forms;

namespace IDS
{
    class MessageSenderThread
    {
        private Status _status;
        private ArrayList _messagesToSend;
        private UDPSecureSocket _socket;
        private ActiveNodes _activeNodes;
        private ArrayList _publishedAttacks;
        private ArrayList _publishedSolutions;
        private Hashtable _receivedAttacks;

        public MessageSenderThread(Status status, ArrayList messagesToSend, ActiveNodes activeNodes, ArrayList publishedAttacks, Hashtable receivedAttacks, ArrayList publishedSolutions, KeysManager km, int port)
        {
            _status = status;
            _messagesToSend = messagesToSend;
            _activeNodes = activeNodes;
            _publishedAttacks = publishedAttacks;
            _receivedAttacks = receivedAttacks;
            _publishedSolutions = publishedSolutions;
            _socket = new UDPSecureSocket(port, km);

            km.SendSocket = _socket;
        }

        public void Run()
        {
            while (_status.IsOnline || MessagesToSendCount() != 0)
            {
                if (MessagesToSendCount() != 0)
                {
                    Object messageToSend;

                    lock (_messagesToSend.SyncRoot)
                    {
                        messageToSend = _messagesToSend[0];
                        _messagesToSend.RemoveAt(0);
                    }

                    if (messageToSend.GetType().ToString() == "CommModule.Messages.NewAttackMessage")
                    {
                        SendNewAttackMessage(messageToSend);
                    }
                    else if (messageToSend.GetType().ToString() == "IDS.Message")
                    {
                        SendDelayedMessage(messageToSend);
                    }
                    else if (messageToSend.GetType().ToString() == "CommModule.Messages.AttackSolutionMessage")
                    {
                        SendAttackSolutionMessage(messageToSend);
                    }
                    else if (messageToSend.GetType().ToString() == "CommModule.Messages.TrackerRequestMessage")
                    {
                        TrackerRequestMessage trackerRequest = (TrackerRequestMessage)messageToSend;
                        _socket.sendMessage(trackerRequest, _status.FirstTrackerAddr, _status.FirstTrackerPort);
                    }
                }
                else
                {
                    Thread.Sleep(20000);
                }
            }
        }

        private void SendNewAttackMessage(Object messageToSend)
        {
            NewAttackMessage attackMessage = (NewAttackMessage)messageToSend;
            PublishedAttack attack = new PublishedAttack(attackMessage);

            IEnumerator activeNodesEnumerator = _activeNodes.ActiveNodesList.GetEnumerator();

            while (activeNodesEnumerator.MoveNext())
            {
                Node node = (Node)activeNodesEnumerator.Current;
                _socket.sendMessage(attackMessage, node.IPAddress, node.port);
                attack.addNode(node);
            }

            lock (_publishedAttacks.SyncRoot)
            {
                _publishedAttacks.Add(attack);
            }
        }

        private void SendDelayedMessage(Object messageToSend)
        {
            Message message = (Message)messageToSend;

            if (message.ConcreteMessage.GetType().ToString() == "IDS.PublishedAttack")
            {
                PublishedAttack attackToSend = (PublishedAttack)message.ConcreteMessage;
                Node nodeToSend = message.NodeToSend;
                _socket.sendMessage(attackToSend.AttackMessage, nodeToSend.IPAddress, nodeToSend.port);
                attackToSend.addNode(nodeToSend);
            }
            else if (message.ConcreteMessage.GetType().ToString() == "CommModule.Messages.AttackSolutionMessage")
            {
                Node nodeToSend = message.NodeToSend;
                _socket.sendMessage(message.ConcreteMessage, nodeToSend.IPAddress, nodeToSend.port);
            }
        }

        private void SendAttackSolutionMessage(Object messageToSend)
        {
            AttackSolutionMessage attackSolution = (AttackSolutionMessage)messageToSend;

            Node nodeToSend = new Node();
            bool isValidAttack = false;

            lock (_receivedAttacks.SyncRoot)
            {
                IDictionaryEnumerator receivedAttacksEnum = _receivedAttacks.GetEnumerator();

                while (receivedAttacksEnum.MoveNext())
                {
                    NewAttackMessage newAttack = (NewAttackMessage)receivedAttacksEnum.Value;
                    if (newAttack.AttackId == attackSolution.AttackId)
                    {
                        nodeToSend.IPAddress = newAttack.SourceIdsAddress;
                        nodeToSend.port = newAttack.SourceIdsPort;
                        isValidAttack = true;
                        break;
                    }
                }
            }

            if (!isValidAttack)
            {
                MessageBox.Show("The attack Identifier provided do not correspond to a valid communicated " +
                    "attack.", "Input Error");
                return;
            }

            if (_activeNodes.IsNodeActive(nodeToSend))
            {
                _socket.sendMessage(attackSolution, nodeToSend.IPAddress, nodeToSend.port);
            }
            else
            {
                Message message = new Message(nodeToSend, attackSolution);

                lock (_publishedSolutions.SyncRoot)
                {
                    _publishedSolutions.Add(message);
                }
            }
        }

        private int MessagesToSendCount()
        {
            lock (_messagesToSend.SyncRoot)
            {
                return _messagesToSend.Count;
            }
        }
    }
    
}