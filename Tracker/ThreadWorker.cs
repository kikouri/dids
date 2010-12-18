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
        
        // Do not alter bellow if you don't know what you are doing
        private KeysManager keyManager;

        private Hashtable NotSynchronizedActiveNodesList;
        private DateTime timestampLastUpdate = DateTime.MinValue;
        private int listeningPort;
        private int sendingPort;
        private UDPSecureSocket receiveSecureSocket;
        private UDPSecureSocket sendSecureSocket;
        private TrackerRequestMessage tr;
        private ArrayList workForWorkers = new ArrayList();
        private SlaveMaster slaveMaster;

        // Locks
        private Object myLock = new Object();
        public object sharedLock;
        private Object slaveIdLock = new Object();

        public ThreadWorker(int listeningPort, int sendingPort,UDPSecureSocket listeningSocket, UDPSecureSocket sendingSocket, KeysManager keyManager)
        {
            setActiveNodeList(new Hashtable());
            sharedLock = new object();

            this.keyManager = keyManager;
            this.listeningPort = listeningPort;
            this.sendingPort = sendingPort;
            /*
            try
            {
                receiveSecureSocket = new UDPSecureSocket(listeningPort);
            }
            catch (Exception e)
            {
                Console.WriteLine("[ThreadWorker] Could not create socket on port " + listeningPort);
                Console.WriteLine(e.Message);
                System.Environment.Exit(-1);
            }
            */
            receiveSecureSocket = listeningSocket;
            sendSecureSocket = sendingSocket;
            slaveMaster = new SlaveMaster(this, sharedLock, NumSlaves, sendingPort, sendSecureSocket);
            slaveMaster.startWorkers();

            Thread f = new Thread(initiateForm);
            f.IsBackground = true;
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
            lock (myLock)
            {
                getWorkForWorkers().Add(trm);
            }
        }

        private void removeNode(String idIDS)
        {
                //getActiveNodeList().Remove(String.Concat(ipaddress, port));
            try
            {
                getActiveNodeList().Remove(idIDS);
            }
            catch (Exception e)
            {
                Console.WriteLine("[Janitor] Problem on removing " + idIDS);
                Console.WriteLine(e.Message);
                return;
            }
        }

        public bool hasWork()
        {
            if (getWorkForWorkers().Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                else
                {
                    return null;
                }
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
                Console.WriteLine("[ThreadWorker] listening at " + listeningPort);
                tr = (TrackerRequestMessage)receiveSecureSocket.receiveMessage();
                Console.WriteLine("[ThreadWorker] Request ( id: " + tr.Idids + " Address: " + tr.Address + ":" + tr.Port + " TS: " + tr.Ts + ")");
                addWork(tr);
                Monitor.Enter(sharedLock);
                Monitor.Pulse(sharedLock);
                Monitor.Exit(sharedLock);
            }
        }

        public void createCleaner()
        {
            Thread t = new Thread(doCleaning);
            t.IsBackground = true;
            t.Start();
        }

        /*
         * Perform the removal of the nodes that did not do an imAlive for a certain time
         */
        public void doCleaning()
        {
            while (true)
            {
                Console.WriteLine("[Janitor] starting cleanup.");
                TimeSpan cleaningThreshold = new TimeSpan(cleaningHours, cleaningMinutes, cleaningSeconds);
                DateTime now = DateTime.Now;
                ArrayList activeNodes = hashTableToArray();
                foreach (Node n in activeNodes)
                {
                    TimeSpan diffTimestamp = now.Subtract(n.LastTime);
                    if (diffTimestamp.CompareTo(cleaningThreshold) > 0)
                    {
                        Console.WriteLine("[Janitor] removing " + n.idIDS + " " + n.IPAddress + ":" + n.port);
                        removeNode(n.idIDS);
                        //removeNode(n.IPAddress, n.port);
                        //getActiveNodeList().Remove(String.Concat(n.IPAddress, n.port));
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
        public TrackerAnswerMessage imAlive(String _idIds, String _ipaddress, int _port, DateTime _ts)
        {
            if (!isValid(_idIds,_ipaddress, _port, _ts))
            {
                return new TrackerAnswerMessage(-1);
            }
            //Console.WriteLine("received ts: " + _ts + " updatedTs: " + timestampLastUpdate);
            //String key = String.Concat(_ipaddress, _port);
            String key = _idIds;
            if (!getActiveNodeList().Contains(key))
            {
                addActiveNode(_idIds,_ipaddress, _port);
                timestampLastUpdate = DateTime.Now;
                return new TrackerAnswerMessage(1,hashTableToArray(),timestampLastUpdate);
            }
            else
            {
                ((Node)getActiveNodeList()[key]).LastTime = DateTime.Now;
                if (_ts == null || _ts.CompareTo(timestampLastUpdate) < 0)
                {
                    return new TrackerAnswerMessage(1,hashTableToArray(), timestampLastUpdate);
                }
            }
            return new TrackerAnswerMessage(0);
        }

        /*
         * Used to check if a given request is valid or not.
         * @return true if are valid parameters, false otherwise
         */
        private bool isValid(String idIDS, String ipaddress, int port, DateTime dt)
        {
            if (idIDS == null || ipaddress == null || port < 0 || port > 65535 || dt == null)
            {
                return false;
            }
            return true;
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
        private void addActiveNode(String idIDS, String _ipaddress, int _port)
        {
            Node node = new Node(idIDS,_ipaddress, _port);
            //String key = String.Concat(_ipaddress, _port);
            String key = idIDS;
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