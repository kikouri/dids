using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using CommModule.Messages;

namespace IDS
{
    class StatusSenderThread
    {
        private Status _status;
        private ArrayList _messagesToSend;
        private ActiveNodes _activeNodes;
        
        public StatusSenderThread(Status status, ArrayList messagesToSend, ActiveNodes activeNodes)
        {
            _status = status;
            _messagesToSend = messagesToSend;
            _activeNodes = activeNodes;
        }

        public void Run()
        {
            while (_status.IsOnline)
            {
                TrackerRequestMessage request = new TrackerRequestMessage(_status.Node.IPAddress, _status.Node.port, _activeNodes.LastUpdateTimestamp, _status.IdsID);

                lock (_messagesToSend.SyncRoot)
                {
                    _messagesToSend.Add(request);
                }

                Thread.Sleep(10000);
            }
        }
    }
}
