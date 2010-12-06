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
        private Hashtable activeNodesList;
        private DateTime timestampLastUpdate;
        private int listeningPort;
        private UDPSecureSocket secureSocket;
        private TrackerAnswerMessage ta;
        private TrackerRequestMessage tr;

        public ThreadWorker(int listeningPort)
        {
            activeNodesList = new Hashtable();
            this.listeningPort = listeningPort;
            secureSocket = new UDPSecureSocket(listeningPort);
        }

        /*
         * The main function
         */
        public void Listener()
        {
            while (true)
            {
                Console.WriteLine("[ThreadWorker] Waiting for request at " + listeningPort);
                tr = (TrackerRequestMessage)secureSocket.receiveMessage();
                Console.WriteLine("[ThreadWorker] Request (" + tr.Address + ":" + tr.Port + " ts: " + tr.Ts + ")");
                ta = new TrackerAnswerMessage();
                ta.ActiveNodeList = imAlive(tr.Address, tr.Port, tr.Ts);
                secureSocket.sendMessage((object)ta, tr.Address, tr.PortToAnswer); // o pedidor vai receber 
            }
        }

        /* 
         * The I'm alive function called by any IDS.
         * IDS sends his IPAddress, his port and the time on which his active node list was synchronized (default is zero)
         * This function will be used for registration as well
         * @return the active list if needed, or null otherwise
         */
        public ArrayList imAlive(IPAddress _ipaddress, int _port, DateTime _ts)
        {
            if (!activeNodesList.Contains(_ipaddress))
            {
                addActiveNode(_ipaddress, _port);
                timestampLastUpdate = DateTime.Now;
                return hashTableToArray();
            }
            else
            {
                if (ts.CompareTo(timestampLastUpdate) < 0)
                {
                    return hashTableToArray();
                }
            }
            return null;
        }


        /*
         * Converts the hashtable to an array
         * @return the arraylist with all elements
         */
        private ArrayList hashTableToArray()
        {
            ArrayList temp = new ArrayList();
            IDictionaryEnumerator en = activeNodesList.GetEnumerator();
            while (en.MoveNext())
            {
                temp.Add((IPEndPoint)en.Value);
            }
            return temp;
        }

        /* 
         * Adding an element to the list
         * used by imAlive only
         */
        private void addActiveNode(IPAddress _ipaddress, int _port)
        {
            //Node node = new Node(IPAddress, _port);
            IPEndPoint ipep = new IPEndPoint(_ipaddress, _port);
            String key = String.Concat(_ipaddress.ToString(), _port);
            activeNodesList.Add(key, ipep);
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