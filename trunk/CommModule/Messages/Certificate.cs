using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommModule.Messages
{
    class Certificate
    {
        private long _serialNumber;

        private string _issuer;

        private DateTime _validity;

        private string _subject;

        private string _subjectPublicKey;

        private string _signature;

        public Certificate(long serial, string issuer, DateTime val, string subject, string publicKey, string signature)
        {
            _serialNumber = serial;
            _issuer = issuer;
            _validity = val;
            _subject = subject;
            _subjectPublicKey = publicKey;
            _signature = signature;
        }
    }
}
