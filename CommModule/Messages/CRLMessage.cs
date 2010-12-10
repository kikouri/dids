using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommModule.Messages
{
    /*
     * Works as a request and a answer message.
     */
    public class CRLMessage
    {
        private long _serialNumber;

        //Only used in the answer.
        private bool _isRevocated;

        //Only used in the request.
        private string _adressToAnswer;

        //Only used in the request.
        private int _portToAnswer;
        
        public CRLMessage()
        {
        }

        public CRLMessage(long serialNumber, string address, int port)
        {
            _serialNumber = serialNumber;
            _isRevocated = false;
            _adressToAnswer = address;
            _portToAnswer = port;
        }

        public long SerialNumber
        {
            get { return _serialNumber; }
            set { _serialNumber = value; }
        }

        public bool IsRevocated
        {
            get { return _isRevocated; }
            set { _isRevocated = value; }
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
