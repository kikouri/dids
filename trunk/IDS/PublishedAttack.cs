using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommModule.Messages;
using System.Collections;

namespace IDS
{
    [Serializable()]
    public class PublishedAttack
    {
        private NewAttackMessage _attackMessage;
        private Hashtable _nodesSent;
        private DateTime _attackPublicationExpiration;

        public PublishedAttack(NewAttackMessage attackMessage)
        {
            _attackMessage = attackMessage;
            _nodesSent = Hashtable.Synchronized(new Hashtable());
            _attackPublicationExpiration = DateTime.Now;
            _attackPublicationExpiration.AddMonths(3);
        }

        public NewAttackMessage AttackMessage
        {
            get { return _attackMessage; }
            set { _attackMessage = value; }
        }

        public void addNode(Node node)
        {
            lock (_nodesSent.SyncRoot)
            {
                _nodesSent.Add(node.IPAddress + node.port.ToString(), node);
            }
        }

        public bool attackSentToNode(Node node)
        {
            lock (_nodesSent.SyncRoot)
            {
                return _nodesSent.ContainsKey(node.IPAddress + node.port.ToString());
            }
        }

        public DateTime AttackPublicationExpiration
        {
            get { return _attackPublicationExpiration; }
            set { _attackPublicationExpiration = value; }
        }
    }
}
