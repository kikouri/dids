using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommModule.Messages
{
    public class GenericMessage
    {
        private String _objectType;
        private String _objectString;

        public GenericMessage(String objectType, String objectString)
        {
            _objectType = objectType;
            _objectString = objectString;
        }

        public GenericMessage()
        {
        }

        public String ObjectType
        {
            get { return _objectType; }
            set { _objectType = value; }
        }

        public String ObjectString
        {
            get { return _objectString; }
            set { _objectString = value; }
        }

    }
}
