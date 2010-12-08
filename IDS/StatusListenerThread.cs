using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using CommModule.Messages;

namespace IDS
{
    class StatusListenerThread
    {
        private Status _status;
        private ArrayList _statusMessages;
        private ActiveNodes _activeNodes;

        public StatusListenerThread(Status status, ArrayList statusMessages, ActiveNodes activeNodes)
        {
            _status = status;
            _statusMessages = statusMessages;
            _activeNodes = activeNodes;
        }

        public void Run()
        {
            while (_status.IsOnline)
            {
                if (_statusMessages.Count != 0)
                {
                }
                else
                {
                    Thread.Sleep(20000);
                }
            }
        }
    }
}
