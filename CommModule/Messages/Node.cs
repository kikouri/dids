using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace CommModule.Messages
{
    public class Node
    {
        
        private string _IPAddress;        
        private int _port;

        public Node(string IPAddress, int port)
        {
            this.IPAddress = IPAddress;
            this.port = port;
        }

        public Node()
        {
        }

        [XmlElement(ElementName = "IPAddress")]
        public String IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }

        [XmlElement(ElementName = "Port")]
        public int port
        {
            get { return _port; }
            set { _port = value; }
        }

    }
}
