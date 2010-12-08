using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDS
{
    class Status
    {
        private bool _isOnline;
        private int _portToReceive;
        private String _idsID;
        private Object _lockObject = new Object();

        public Status()
        {
            _isOnline = true;
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

        public int PortToReceive
        {
            get
            {
                lock (_lockObject)
                {
                    return _portToReceive;
                }
            }
            set
            {
                lock (_lockObject)
                {
                    _portToReceive = value;
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