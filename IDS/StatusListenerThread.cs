using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using CommModule.Messages;

namespace IDS
{
    class StatusListenerThread
    {
        private Status _status;
        private ArrayList _statusMessages;
        private ArrayList _publishedAttacks;
        private ArrayList _publishedSolutions;
        private ArrayList _messagesToSend;
        private ActiveNodes _activeNodes;

        public StatusListenerThread(Status status, ArrayList statusMessages, ActiveNodes activeNodes, ArrayList publishedAttacks, ArrayList messagesToSend, ArrayList publishedSolutions)
        {
            _status = status;
            _statusMessages = statusMessages;
            _activeNodes = activeNodes;
            _publishedAttacks = publishedAttacks;
            _publishedSolutions = publishedSolutions;
            _messagesToSend = messagesToSend;
        }

        public void Run()
        {
            while (_status.IsOnline)
            {
                int count;

                lock (_statusMessages.SyncRoot)
                {
                    count = _statusMessages.Count;
                }

                if (count != 0)
                {
                    TrackerAnswerMessage trackerAnswer;

                    lock (_statusMessages.SyncRoot)
                    {
                        trackerAnswer = (TrackerAnswerMessage)_statusMessages[0];
                        _statusMessages.RemoveAt(0);
                    }

                    if (trackerAnswer.NewUpdateTime.CompareTo(_activeNodes.LastUpdateTimestamp) > 0)
                    {
                        UpdateNodesList(trackerAnswer);                        
                    }
                }
                else
                {
                    Thread.Sleep(20000);
                }
            }
        }

        private void UpdateNodesList(TrackerAnswerMessage trackerAnswer)
        {
            if (trackerAnswer.ActiveNodeList.Count > 0)
            {
                Hashtable newNodes = new Hashtable();

                IEnumerator newNodesEnumerator = trackerAnswer.ActiveNodeList.GetEnumerator();

                while (newNodesEnumerator.MoveNext())
                {
                    Node node = (Node)newNodesEnumerator.Current;
                    newNodes.Add(node.IPAddress + node.port.ToString(), node);
                }

                ArrayList oldActiveNodesList = (ArrayList)_activeNodes.ActiveNodesList.Clone();

                IEnumerator oldActiveNodesEnumerator = oldActiveNodesList.GetEnumerator();

                while (oldActiveNodesEnumerator.MoveNext())
                {
                    Node node = (Node)oldActiveNodesEnumerator.Current;
                    newNodes.Remove(node.IPAddress+node.port.ToString());
                }

                VerifyDelayedMessages(newNodes);

                _activeNodes.LastUpdateTimestamp = trackerAnswer.NewUpdateTime;
                _activeNodes.ActiveNodesList = trackerAnswer.ActiveNodeList;
            }
            else
            {
                _activeNodes.LastUpdateTimestamp = trackerAnswer.NewUpdateTime;
                _activeNodes.ActiveNodesList = trackerAnswer.ActiveNodeList;
            }
        }

        private void VerifyDelayedMessages(Hashtable newNodes)
        {
            lock (_publishedAttacks.SyncRoot)
            {
                IEnumerator pubAttacksEnum = _publishedAttacks.GetEnumerator();
                IDictionaryEnumerator newNodesEnum = newNodes.GetEnumerator();
                ArrayList pubAttacksToRemove = new ArrayList();

                while (pubAttacksEnum.MoveNext())
                {
                    PublishedAttack currentAttack = (PublishedAttack)pubAttacksEnum.Current;
                    if (currentAttack.AttackPublicationExpiration.CompareTo(DateTime.Now) <= 0)
                    {
                        pubAttacksToRemove.Add(currentAttack);
                        
                        continue;
                    }

                    while (newNodesEnum.MoveNext())
                    {
                        Node currentNode = (Node)newNodesEnum.Value;
                        if (!currentAttack.attackSentToNode(currentNode))
                        {
                            Message message = new Message(currentNode, currentAttack);
                            lock (_messagesToSend.SyncRoot)
                            {
                                _messagesToSend.Add(message);
                            }
                        }
                    }
                    newNodesEnum.Reset();
                }

                if (pubAttacksToRemove.Count > 0)
                {
                    IEnumerator pubAtRem = pubAttacksToRemove.GetEnumerator();
                    while (pubAtRem.MoveNext())
                    {
                        PublishedAttack pubAtR = (PublishedAttack)pubAtRem.Current;
                        _publishedAttacks.Remove(pubAtR);
                    }
                }
            }

            lock (_publishedSolutions.SyncRoot)
            {
                IEnumerator solutionsEnum = _publishedSolutions.GetEnumerator();

                while (solutionsEnum.MoveNext())
                {
                    Message solutionMessage = (Message)solutionsEnum.Current;
                    Node nodeToSend = solutionMessage.NodeToSend;

                    if (newNodes.ContainsKey(nodeToSend.IPAddress + nodeToSend.port.ToString()))
                    {
                        lock (_messagesToSend.SyncRoot)
                        {
                            _messagesToSend.Add(solutionMessage);
                        }
                        _publishedSolutions.Remove(solutionMessage);
                    }
                }
            }
        }
    }
}