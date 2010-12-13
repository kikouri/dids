using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using CommModule;
using CommModule.Messages;
using System.Net;
using System.Windows.Forms;
/*
 * As there will be no synchronization between trackers (to discuss),
 * We just have to instantiate two (or more) objects of this class, as long as IDSs know the address+port
 * of single one of the trackers
 * 
 */
namespace Tracker
{
    public class ThreadWorker
    {

        // Configuration
        // Number of slaves wanted
        private int NumSlaves = 1;

        // Cleaning Threshold (how many time to let pass to remove a node from active list)
        int cleaningSeconds = 20;
        int cleaningMinutes = 0;
        int cleaningHours = 0;

        // Cleaners' interval to perform... cleaning
        int cleanerSleepTime = 10000;
        
        // Do not alter bellow
        TimeSpan cleaningThreshold = new TimeSpan(cleaningHours, cleaningMinutes, cleaningSeconds);
        private Hashtable NotSynchronizedActiveNodesList;
        private DateTime timestampLastUpdate = DateTime.MinValue;
        private int listeningPort;
        private int sendingPort;
        private UDPSecureSocket secureSocket;
        private TrackerRequestMessage tr;
        private ArrayList workForWorkers = new ArrayList();
        private Object myLock = new Object();
        private Object myTimestapLock = new Object();
        private Object sendingLock = new Object();
        private ArrayList slavesArray = new ArrayList();
        private Object slaveIdLock = new Object();

        private SlaveMaster slaveMaster;
        public object sharedLock;



        public ThreadWorker(int listeningPort, int sendingPort)
        {
            setActiveNodeList(new Hashtable());
            sharedLock = new object();
            this.listeningPort = listeningPort;
            this.sendingPort = sendingPort;
            try
            {
                secureSocket = new UDPSecureSocket(listeningPort);
            }
            catch (Exception e)
            {
                Console.WriteLine("[ThreadWorker] Could not create socket on port " + listeningPort);
                Console.WriteLine(e.Message);
            }
            slaveMaster = new SlaveMaster(this, sharedLock, NumSlaves, sendingPort);
            slaveMaster.startWorkers();
            Thread f = new Thread(initiateForm);
            f.Start();
        }

        public void initiateForm()
        {
            Form1 form1 = new Form1(this);
            Application.Run(form1);
        }

        private ArrayList getWorkForWorkers()
        {
            return ArrayList.Synchronized(workForWorkers);
        }

        private void addWork(TrackerRequestMessage trm)
        {
            getWorkForWorkers().Add(trm);
        }

        public bool hasWork()
        {
            return getWorkForWorkers().Count > 0 ? true : false;
        }

        public TrackerRequestMessage getWork()
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

        /*
         * The main function
         */
        public void Listener()
        {
            // Create the cleaner that will remove the inactive nodes
            createCleaner();
            
            while (true)
            {
                tr = (TrackerRequestMessage)secureSocket.receiveMessage();
                Console.WriteLine("[ThreadWorker] Request (" + tr.Address + ":" + tr.Port + " ts: " + tr.Ts + ")");
                addWork(tr);
                Monitor.Enter(sharedLock);
                Monitor.Pulse(sharedLock);
                Monitor.Exit(sharedLock);
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
                
                TimeSpan threshold = cleaningThreshold;
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
                Thread.Sleep(cleanerSleepTime);
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
        public ArrayList hashTableToArray()
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