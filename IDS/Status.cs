﻿using System;
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
        private String _password;
        private String _firstTrackerAddr;
        private int _firstTrackerPort;
        private String _secondTrackerAddr;
        private int _secondTrackerPort;
        private Object _lockObject = new Object();

        public Status()
        {
            _isOnline = true;
            _isLoggedOn = false;
            _node = new Node();
            IPAddress[] hostIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress hostIP in hostIPs)
            {
                if (hostIP.ToString().StartsWith("192.168.") ||
                    hostIP.ToString().StartsWith("10.") ||
                    hostIP.ToString().StartsWith("172.16"))
                    _node.IPAddress = hostIP.ToString();
            }


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

        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public String FirstTrackerAddr
        {
            get { return _firstTrackerAddr; }
            set { _firstTrackerAddr = value; }
        }

        public int FirstTrackerPort
        {
            get { return _firstTrackerPort; }
            set { _firstTrackerPort = value; }
        }

        public String SecondTrackerAddr
        {
            get { return _secondTrackerAddr; }
            set { _secondTrackerAddr = value; }
        }

        public int SecondTrackerPort
        {
            get { return _secondTrackerPort; }
            set { _secondTrackerPort = value; }
        }

        public void ErasePassword()
        {
            _password.Remove(0);
        }
    }
}