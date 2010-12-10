using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommModule.Messages;

namespace IDS
{
    public class Message
    {
        private Node _node;
        private Object _concreteMessage;

        public Message(Node node, Object concreteMessage)
        {
            _node = node;
            _concreteMessage = concreteMessage;
        }

        public Node NodeToSend
        {
            get { return _node; }
            set { _node = value; }
        }

        public Object ConcreteMessage
        {
            get { return _concreteMessage; }
            set { _concreteMessage = value; }
        }
    }
}
