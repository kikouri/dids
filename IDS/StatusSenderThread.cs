using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace IDS
{
    class StatusSenderThread
    {
        ArrayList _sendList;

        public StatusSenderThread(ArrayList sendList)
        {
            _sendList = sendList;
        }

        public void Start()
        {
            while (true)
            {
                Thread.Sleep(20000);
                //Create Status Message
                //add to list to send
            }
        }
    }
}
