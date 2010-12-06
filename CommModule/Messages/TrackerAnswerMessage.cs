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
        
        private ArrayList _activeNodeList;
        private DateTime _newUpdateTime;

        /*
         * 0 == empty
         * 1 == has activeNodeList and Timestamp
         */
        private int _responseCode;

        public TrackerAnswerMessage(int code, ArrayList activeNodeList, DateTime ts)
        {
            _responseCode = code;
            _activeNodeList = activeNodeList;
            _newUpdateTime = ts;
        }

        public TrackerAnswerMessage()
        {
        }

        public TrackerAnswerMessage(int code)
        {
            _responseCode = code;
        }


        [XmlElement(Type = typeof(Node))]
        public ArrayList ActiveNodeList
        {
            get { return _activeNodeList; }
            set { _activeNodeList = value; }
        }

        public DateTime NewUpdateTime
        {
            get { return _newUpdateTime; }
            set { _newUpdateTime = value; }
        }

        public int ResponseCode
        {
            get { return _responseCode; }
            set { _responseCode = value; }
        }
    }
}
