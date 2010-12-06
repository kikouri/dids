using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace CommModule.Messages
{
    
    public class TrackerAnswerMessage
    {
        [XmlArrayItem(ElementName = "ActiveNodeList", Type = typeof(Node))]
        private ArrayList _activeNodeList;

        public TrackerAnswerMessage(ArrayList activeNodeList)
        {
            _activeNodeList = activeNodeList;
        }

        public TrackerAnswerMessage()
        {
        }
        
        
        
        public ArrayList ActiveNodeList
        {
            get { return _activeNodeList; }
            set { _activeNodeList = value; }
        }

    }
}
