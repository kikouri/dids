using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tracker
{
    class Node
    {
        private string _IPAddress;
        private int _port;

        public Node(string IPAddress, int port)
        {
            this.IPAddress = IPAddress;
            this.port = port;
        }

        public String IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }

        public int port
        {
            get { return _port; }
            set { _port = value; }
        }

    }
}
