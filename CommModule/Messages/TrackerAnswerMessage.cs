using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CommModule.Messages
{
    public class TrackerAnswerMessage
    {
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
