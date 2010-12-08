using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using CommModule.Messages;
using System.Net;

namespace IDS
{
    class StatusSenderThread
    {
        private Status _status;
        private ArrayList _messagesToSend;
        private ActiveNodes _activeNodes;
        private String _hostAddress;

        public StatusSenderThread(Status status, ArrayList messagesToSend, ActiveNodes activeNodes)
        {
            _status = status;
            _messagesToSend = messagesToSend;
            _activeNodes = activeNodes;
            _hostAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
        }

        public void Run()
        {
            while (_status.IsOnline)
            {
                TrackerRequestMessage request = new TrackerRequestMessage(_hostAddress, _status.PortToReceive, _activeNodes.LastUpdateTimestamp, _status.IdsID);

                lock (_messagesToSend.SyncRoot)
                {
                    _messagesToSend.Add(request);
                }

                Thread.Sleep(20000);
            }
        }
    }
}
