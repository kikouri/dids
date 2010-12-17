using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommModule.Messages
{
    public class SessionKeyMessageACK
    {
        private string _ack;
        public string ACK
        {
            get { return _ack; }
        }

        
        public SessionKeyMessageACK()
        {
            _ack = "OK";
        }
    }
}
