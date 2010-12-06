using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
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

        public ThreadWorker(int listeningPort)
        {
            activeNodesList = new Hashtable();
            this.listeningPort = listeningPort;
        }

        /*
         * The main function
         */
        public void Listener()
        {
            // Listen for messages
            // Treat them
            addActiveNode("123", 1);
            addActiveNode("456", 2);
            hashTableToArray();
            Console.ReadLine();
        }

        /* 
         * The I'm alive function called by any IDS.
         * IDS sends his IPAddress, his port and the time on which his active node list was synchronized (default is zero)
         * This function will be used for registration as well
         * @return the active list if needed, or null otherwise
         */
        public ArrayList imAlive(String IPAddress, int port, DateTime ts)
        {
            if (!activeNodesList.Contains(IPAddress))
            {
                addActiveNode(IPAddress, port);
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
                temp.Add((Node)en.Value);
            }
            return temp;
        }

        /* 
         * Adding an element to the list
         * used by imAlive only
         */
        private void addActiveNode(String IPAddress, int port)
        {
            Node node = new Node(IPAddress, port);
            String key = String.Concat(IPAddress, port);
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