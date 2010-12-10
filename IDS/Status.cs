using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using CommModule.Messages;

namespace IDS
{
    public class Status
    {
        private bool _isOnline;
        private bool _isLoggedOn;
        private Node _node;
        private int _publishedAttackMaxId;
        private String _idsID;       
        private Object _lockObject = new Object();

        public Status()
        {
            _isOnline = true;
            _node = new Node();
            _node.IPAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            _publishedAttackMaxId = 0;
        }

        public bool IsOnline
        {
            get
            {
                lock (_lockObject)
                {
                    return _isOnline;
                }
            }
            set
            {
                lock (_lockObject)
                {
                    _isOnline = value;
                }
            }
        }

        public bool IsLoggedOn
        {
            get
            {
                lock (_lockObject)
                {
                    return _isLoggedOn;
                }
            }
            set
            {
                lock (_lockObject)
                {
                    _isLoggedOn = value;
                }
            }
        }

        public Node Node
        {
            get
            {
                return _node;
            }
            set
            {
                lock (_lockObject)
                {
                    _node = value;
                }
            }            
        }

        public int PublishedAttackMaxId
        {
            get
            {
                lock (_lockObject)
                {
                    return _publishedAttackMaxId;
                }
            }
            set
            {
                lock (_lockObject)
                {
                    _publishedAttackMaxId = value;
                }
            }
        }

        public String IdsID
        {
            get { return _idsID; }
            set { _idsID = value; }
        }
    }
}