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

        private string _adressToAnswer;

        private int _portToAnswer;


        public SessionKeyMessage(string key, DateTime val, string addressToAnswer, int portToAnswer)
        {
           _key = key;
           _validity = val;

           _adressToAnswer = addressToAnswer;
           _portToAnswer = portToAnswer;
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

        public string AdressToAnswer
        {
            get { return _adressToAnswer; }
            set { _adressToAnswer = value; }
        }

        public int PortToAnswer
        {
            get { return _portToAnswer; }
            set { _portToAnswer = value; }
        }
    }
}
