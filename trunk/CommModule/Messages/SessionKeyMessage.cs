using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommModule.Messages
{

    public class SessionKeyMessage
    {
        private string _key;

        private DateTime _validity;


        public SessionKeyMessage(string key, DateTime val)
        {
           _key = key;
           _validity = val;
        }

        public SessionKeyMessage()
        {
        }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public DateTime Validity
        {
            get { return _validity; }
            set { _validity = value; }
        }
    }
}
