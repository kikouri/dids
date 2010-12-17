using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommModule.Messages
{
    public class CertificateRequestMessage
    {
        private string _adressToAnswer;

        private int _portToAnswer;

        private Certificate _myCertificate;

        
        public CertificateRequestMessage(string addressToAnswer, int portToAnswer, Certificate myCertificate)
        {
            _adressToAnswer = addressToAnswer;
            _portToAnswer = portToAnswer;

            _myCertificate = myCertificate;
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

        public Certificate MyCertificate
        {
            get { return _myCertificate; }
            set { _myCertificate = value; }
        }

    }
}
