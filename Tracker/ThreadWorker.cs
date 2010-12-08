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
        private DateTime timestampLastUpdate = DateTime.MinValue;
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
            imAlive("123.456.789", 69, DateTime.Now);
            imAlive("223.456.789", 29, DateTime.Now);
            while (true)
            {
                Console.WriteLine("[ThreadWorker] Newest timestamp is " + timestampLastUpdate);
                Console.WriteLine("[ThreadWorker] Waiting for request at " + listeningPort);
                tr = (TrackerRequestMessage)secureSocket.receiveMessage();
                Console.WriteLine("[ThreadWorker] Request (" + tr.Address + ":" + tr.Port + " ts: " + tr.Ts + ")");
                ta = imAlive(tr.Address, tr.Port, tr.Ts);
                if (ta == null)
                {
                    Console.WriteLine("[ThreadWorker] No update on activeNodeList found.");
                }
                else
                {
                    ta.NewUpdateTime = timestampLastUpdate;
                    Console.WriteLine("[ThreadWorker] ActiveNodeList was updated and sent with ts: " + ta.NewUpdateTime);

                 }
                secureSocket.sendMessage((object)ta, tr.Address, tr.Port); // o pedidor vai receber 
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
            if (!activeNodesList.Contains(String.Concat(_ipaddress,_port)))
            {
                addActiveNode(_ipaddress, _port);
                timestampLastUpdate = DateTime.Now;
                return new TrackerAnswerMessage(1,hashTableToArray(),timestampLastUpdate);
            }
            else
            {
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
            IDictionaryEnumerator en = activeNodesList.GetEnumerator();
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
            activeNodesList.Add(key, node);
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