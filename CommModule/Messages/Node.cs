using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace CommModule.Messages
{
    [Serializable()]
    public class Node
    {
        
        private string _IPAddress;        
        private int _port;
        private string _idIDS;

        // Last known time of aliveness
        private DateTime _lastTime = DateTime.MinValue;

        // Deprecated: Use the constructor with the idIDS
        public Node(string IPAddress, int port)
        {
            this.IPAddress = IPAddress;
            this.port = port;
            _lastTime = DateTime.Now;
        }

        public Node(string idIDS, string IPAddress, int port)
        {
            this.IPAddress = IPAddress;
            this.port = port;
            this.idIDS = idIDS;
            _lastTime = DateTime.Now;
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

        public String idIDS
        {
            get { return _idIDS; }
            set { _idIDS = value; }
        }

        [XmlElement(ElementName = "Port")]
        public int port
        {
            get { return _port; }
            set { _port = value; }
        }

        [XmlElement(ElementName = "LastTime")]
        public DateTime LastTime
        {
            get { return _lastTime; }
            set { _lastTime = value; }
        }

        public string toString()
        {
            return _IPAddress + ":" + _port;
        }

    }
}
