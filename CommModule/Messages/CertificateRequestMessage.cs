using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommModule.Messages
{
    class CertificateRequestMessage
    {
        private string _adressToAnswer;

        private int _portToAnswer;
        
        public CertificateRequestMessage(string addressToAnswer, int portToAnswer)
        {
            _adressToAnswer = addressToAnswer;
            _portToAnswer = portToAnswer;
        }

        public CertificateRequestMessage()
        {}

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
