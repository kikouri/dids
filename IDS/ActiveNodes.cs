using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CommModule.Messages;

namespace IDS
{
    class ActiveNodes
    {
        private DateTime _lastUpdateTimestamp;
        private ArrayList _activeNodesList;
        private Object _lockingObject;

        public ActiveNodes()
        {
            _lastUpdateTimestamp = DateTime.MinValue;
            _activeNodesList = new ArrayList();
            _lockingObject = new Object();
        }

        public DateTime LastUpdateTimestamp
        {
            get
            {
                lock (_lockingObject)
                {
                    return _lastUpdateTimestamp;
                }
            }
            set
            {
                lock (_lockingObject)
                {
                    _lastUpdateTimestamp = value;
                }
            }
        }

        public ArrayList ActiveNodesList
        {
            get
            {
                lock (_lockingObject)
                {
                    return _activeNodesList;
                }
            }
            set
            {
                lock (_lockingObject)
                {
                    _activeNodesList = value;
                }
            }
        }

        public bool IsNodeActive(Node node)
        {
            lock (_lockingObject)
            {
                IEnumerator nodesEnum = _activeNodesList.GetEnumerator();

                while (nodesEnum.MoveNext())
                {
                    Node currNode = (Node)nodesEnum.Current;
                    if (currNode.IPAddress == node.IPAddress && currNode.port == node.port)
                        return true;
                }
                return false;
            }
        }
    }
}
