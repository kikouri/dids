using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommModule.Messages;
using CommModule;
using System.Collections;

namespace Tracker
{
    public class SlaveMaster
    {
        private ThreadWorker threadWorker;
        private object sharedLock;
        private UDPSecureSocket sendSecureSocket;
        private ArrayList listOfWorkers = new ArrayList();
        private ArrayList listOfWork = new ArrayList();
        private int numSlaves;
        private object myLock = new object();
        private int slaveId = 1;
        private int sendPort;

        public SlaveMaster(ThreadWorker threadWorker, object sharedLock, int numSlaves,int sendPort)
        {
            this.threadWorker = threadWorker;
            this.numSlaves = numSlaves;
            this.sharedLock = sharedLock;
            this.sendPort = sendPort;
            sendSecureSocket = new UDPSecureSocket(sendPort);
        }

        public int getId()
        {
            int i;
            lock (myLock)
            {
                i = slaveId;
                slaveId++;
            }
            return i;
        }

        public void startWorkers()
        {
            int i = 0;
            Thread t = new Thread(work);
            for (; i < numSlaves; i++)
            {
                t = new Thread(work);
                t.Start();
                listOfWorkers.Add(t);
            }
        }

        public void endWorkers()
        {
            foreach(Thread t in listOfWorkers)
            {
                t.Abort();
            }
        }

        public void work()
        {
            int id = getId();
            TrackerRequestMessage trm;
            TrackerAnswerMessage tam;

            while(true)
            {
                Monitor.Enter(sharedLock);
                if(!threadWorker.hasWork())
                {
                    Monitor.Wait(sharedLock);
                    continue;
                }
                else
                {
                    trm = threadWorker.getWork();
                    Monitor.Exit(sharedLock);
                    if(trm == null)
                    {
                        continue;
                    }
                }
                lock(myLock)
                {
                    tam = threadWorker.imAlive(trm.Address, trm.Port, trm.Ts);
                    sendSecureSocket.sendMessage((object)tam,trm.Address,trm.Port);
                }
            }
        }
    }
}
