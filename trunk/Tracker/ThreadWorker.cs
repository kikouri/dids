using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using CommModule;
using CommModule.Messages;
using System.Net;
/*
 * As there will be no synchronization between trackers (to discuss),
 * We just have to instantiate two (or more) objects of this class, as long as IDSs know the address+port
 * of single one of the trackers
 * 
 */
namespace Tracker
{
    class ThreadWorker
    {
        private Hashtable NotSynchronizedActiveNodesList;
        private DateTime timestampLastUpdate = DateTime.MinValue;
        private int listeningPort;
        private int sendingPort;
        private UDPSecureSocket secureSocket;
        private UDPSecureSocket sendSecureSocket;
        //private TrackerAnswerMessage ta;
        private TrackerRequestMessage tr;
        private ArrayList workForWorkers = new ArrayList();
        private Object myLock = new Object();
        private Object myTimestapLock = new Object();
        private Object sendingLock = new Object();
        private ArrayList slavesArray = new ArrayList();
        private int NumSlaves = 3;

        public ThreadWorker(int listeningPort, int sendingPort)
        {
            setActiveNodeList(new Hashtable());
            this.listeningPort = listeningPort;
            this.sendingPort = sendingPort;
            secureSocket = new UDPSecureSocket(listeningPort);
            sendSecureSocket = new UDPSecureSocket(sendingPort);
        }

        private ArrayList getWorkForWorkers()
        {
            return ArrayList.Synchronized(workForWorkers);
        }

        private void addWork(TrackerRequestMessage trm)
        {
            getWorkForWorkers().Add(trm);
        }

        private bool hasWork()
        {
            return getWorkForWorkers().Count > 0 ? true : false;
        }

        /* test if trm is null */
        private TrackerRequestMessage getWork()
        {
            lock (myLock)
            {
                if (hasWork())
                {
                    TrackerRequestMessage trm = (TrackerRequestMessage)getWorkForWorkers()[0];
                    getWorkForWorkers().RemoveAt(0);
                    return trm;
                }
                return null;
            }
        }

        private Hashtable getActiveNodeList()
        {
            return Hashtable.Synchronized(NotSynchronizedActiveNodesList);
        }

        private void setActiveNodeList(Hashtable ht)
        {
            NotSynchronizedActiveNodesList = ht;
        }

        public void slave()
        {
            while (true)
            {
                TrackerRequestMessage trm = getWork();
                if (trm != null)
                {
                    Console.WriteLine("[Slave] responding to " + trm.Address + " : " + trm.Port);
                    TrackerAnswerMessage tam;
                    tam = imAlive(trm.Address, trm.Port, trm.Ts);
                    /*
                    if (ta == null)
                    {
                        Console.WriteLine("[ThreadWorker] No update on activeNodeList found.");
                    }
                    else
                    {
                        ta.NewUpdateTime = timestampLastUpdate;
                        Console.WriteLine("[ThreadWorker] ActiveNodeList was updated and sent with ts: " + ta.NewUpdateTime);
                    }
                    */
                    try
                    {
                        lock (sendingLock)
                        {
                            sendSecureSocket.sendMessage((object)tam, trm.Address, trm.Port);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[EXCEPTION] " + e.Message);
                    }
                }
                Thread.Sleep(1000);
            }
        }

        /*
         * The main function
         */
        public void Listener()
        {
            Thread slv;
            // Create cleaner
            createCleaner();

            // Start slaves
            for (int i = 0; i < NumSlaves; i++)
            {
                slv = new Thread(slave);
                slv.Start();
                slavesArray.Add(slv);
            }

            while (true)
            {
                Console.WriteLine("[ThreadWorker] Newest timestamp is " + timestampLastUpdate);
                Console.WriteLine("[ThreadWorker] Waiting for request at " + listeningPort);
                tr = (TrackerRequestMessage)secureSocket.receiveMessage();
                Console.WriteLine("[ThreadWorker] Request (" + tr.Address + ":" + tr.Port + " ts: " + tr.Ts + ")");
                //ta = imAlive(tr.Address, tr.Port, tr.Ts);
                addWork(tr);
                //secureSocket.sendMessage((object)ta, tr.Address, tr.Port); // o pedidor vai receber 
            }
        }

        public void createCleaner()
        {
            Thread t = new Thread(doCleaning);
            t.Start();
        }

        public void doCleaning()
        {
            while (true)
            {
                Console.WriteLine("[Janitor] starting cleanup.");
                // Threshold is 20 seconds before removal
                TimeSpan threshold = new TimeSpan(0, 0, 20);
                DateTime now = DateTime.Now;
                ArrayList activeNodes = hashTableToArray();
                foreach (Node n in activeNodes)
                {
                    TimeSpan diffTimestamp = now.Subtract(n.LastTime);
                    if (diffTimestamp.CompareTo(threshold) > 0)
                    {
                        Console.WriteLine("[Janitor] removing " + n.IPAddress + " " + n.port);
                        getActiveNodeList().Remove(String.Concat(n.IPAddress, n.port));
                        timestampLastUpdate = DateTime.Now;
                    }
                }
                Console.WriteLine("[Janitor] going to sleep.");
                Thread.Sleep(10000);
            }
        }

        /* 
         * The I'm alive function called by any IDS.
         * IDS sends his IPAddress, his port and the time on which his active node list was synchronized (default is zero)
         * This function will be used for registration as well
         * @return the active list if needed, or null otherwise
         */
        public TrackerAnswerMessage imAlive(String _ipaddress, int _port, DateTime _ts)
        {
            Console.WriteLine("received ts: " + _ts + " updatedTs: " + timestampLastUpdate);
            if (!getActiveNodeList().Contains(String.Concat(_ipaddress,_port)))
            {
                addActiveNode(_ipaddress, _port);
                timestampLastUpdate = DateTime.Now;
                return new TrackerAnswerMessage(1,hashTableToArray(),timestampLastUpdate);
            }
            else
            {
                String key = String.Concat(_ipaddress, _port);
                ((Node)getActiveNodeList()[key]).LastTime = DateTime.Now;
                if (_ts == null || _ts.CompareTo(timestampLastUpdate) < 0)
                {
                    return new TrackerAnswerMessage(1,hashTableToArray(), timestampLastUpdate);
                }
            }
            return new TrackerAnswerMessage(0);
        }


        /*
         * Converts the hashtable to an array
         * @return the arraylist with all elements
         */
        private ArrayList hashTableToArray()
        {
            ArrayList temp = new ArrayList();
            IDictionaryEnumerator en = getActiveNodeList().GetEnumerator();
            while (en.MoveNext())
            {
              temp.Add((Node)en.Value);
            }
            return temp;
        }

        /* 
         * Adding an element to the list
         * used by imAlive only
         */
        private void addActiveNode(String _ipaddress, int _port)
        {
            Node node = new Node(_ipaddress, _port);
            //IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(_ipaddress), _port);
            String key = String.Concat(_ipaddress, _port);
            getActiveNodeList().Add(key, node);
            serialize();
        }
        
        /*
         * Serialize to harddrive, if needed
         * @return true if serialization was successful
         */
        private bool serialize()
        {
            return false;
        }
    }
}