﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommModule.Messages
{
    public class Certificate
    {
        private long _serialNumber;

        private string _issuer;

        private DateTime _validity;

        private string _subject;

        private string _subjectPublicKey;

        private string _signature;

        private string _subjectAddress;

        public Certificate(long serial, string issuer, DateTime val, string subject, string publicKey)
        {
            _serialNumber = serial;
            _issuer = issuer;
            _validity = val;
            _subject = subject;
            _subjectPublicKey = publicKey;
        }

        public Certificate()
        {
        }

        public void sign(string key)
        {
            Cryptography.signCertificate(this, key);
        }

        public string toString()
        {
            string s = "Serial Number: " + _serialNumber + "\n";
            s += "Issuer: " + _issuer + "\n";
            s += "Validity: " + _validity.ToString() + "\n";
            s += "Subject: " + _subject + "\n";
            s += "Subject Public Key: " + _subjectPublicKey + "\n";
            s += "Signature: " + _signature;

            return s;
        }

        public long SerialNumber
        {
            get { return _serialNumber; }
            set { _serialNumber = value; }
        }

        public string Issuer
        {
            get { return _issuer; }
            set { _issuer = value; }
        }

        public DateTime Validity
        {
            get { return _validity; }
            set { _validity = value; }
        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public string SubjectAddress
        {
            get { return _subjectAddress; }
            set { _subjectAddress = value; }
        }

        public string SubjectPublicKey
        {
            get { return _subjectPublicKey; }
            set { _subjectPublicKey = value; }
        }

        public string Signature
        {
            get { return _signature; }
            set { _signature = value; }
        }
    }
}
