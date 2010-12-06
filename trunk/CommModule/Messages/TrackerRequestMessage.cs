using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace CommModule.Messages
{
    public class TrackerRequestMessage
    {
        private DateTime _ts;
        private String _address;
        private int _port;
        private int _portToAnswer;
        private String _idids;

        public TrackerRequestMessage(String address, int port, DateTime ts,int portToAnswer)
            _ts = ts;
            _port = port;
            _address = address;
            _portToAnswer = portToAnswer;
            _idids = idids;
        }

        public TrackerRequestMessage()
        {

        }

        public int PortToAnswer
        {
            get { return _portToAnswer; }
            set { _portToAnswer = value; }
        }

        public String Idids
        {
            get { return _idids; }
            set { _idids = value; }
        }

        public DateTime Ts
        {
            get { return _ts; }
            set { _ts = value; }
        }

        public String Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
    }
}
