using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace CommModule.Messages
{
    public class CertificateGenerationRequest
    {
        private long _referenceNumber;

        private string _publicKey;

        private string _adressToAnswer;

        private int _portToAnswer;
        
        public CertificateGenerationRequest()
        {
        }

        public CertificateGenerationRequest(long referenceNuber, string publicKey, string addressToanswer, int portToAnswer)
        {
            _referenceNumber = referenceNuber;
            _publicKey = publicKey;
            _adressToAnswer = addressToanswer;
            _portToAnswer = portToAnswer;
        }

        public long ReferenceNumber
        {
            get { return _referenceNumber; }
            set { _referenceNumber = value; }
        }

        public string PublicKey
        {
            get { return _publicKey; }
            set { _publicKey = value; }
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
