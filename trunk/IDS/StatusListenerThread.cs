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
                int count;

                lock (_statusMessages.SyncRoot)
                {
                    count = _statusMessages.Count;
                }

                if (count != 0)
                {
                    TrackerAnswerMessage trackerAnswer;

                    lock (_statusMessages.SyncRoot)
                    {
                        trackerAnswer = _statusMessages[0];
                        _statusMessages.RemoveAt(0);
                    }

                    if (trackerAnswer.NewUpdateTime.CompareTo(_activeNodes.LastUpdateTimestamp) > 0)
                    {
                        _activeNodes.LastUpdateTimestamp = trackerAnswer.NewUpdateTime;
                        _activeNodes.ActiveNodesList = trackerAnswer.ActiveNodeList;
                        
                    }
                }
                else
                {
                    Thread.Sleep(20000);
                }
            }
        }
    }
}
