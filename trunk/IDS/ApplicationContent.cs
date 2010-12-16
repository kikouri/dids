using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using CommModule.Messages;

namespace IDS
{   [Serializable()]
    public class ApplicationContent
    {
        private Hashtable _receivedAttacks;
        private ArrayList _publishedAttacks;
        private ArrayList _publishedSolutions;
        private ArrayList _receivedSolutions;

        public Hashtable ReceivedAttacks
        {
            get { return _receivedAttacks; }
            set { _receivedAttacks = value; }
        }

        public ArrayList PublishedAttacks
        {
            get { return _publishedAttacks; }
            set { _publishedAttacks = value; }
        }

        public ArrayList PublishedSolutions
        {
            get { return _publishedSolutions; }
            set { _publishedSolutions = value; }
        }

        public ArrayList ReceivedSolutions
        {
            get { return _receivedSolutions; }
            set { _receivedSolutions = value; }
        }

        public ApplicationContent() { }
    }
}
