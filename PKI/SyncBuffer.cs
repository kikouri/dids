using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PKI
{
    /*
     * Buffer to contain information shared by producer and consumer threads.
     * It's thread safe and prevents any kind of polling by the threads.
     */
    public class SyncBuffer
    {
        private LinkedList<object> _list;

        //Kind of a semaphore...
        private AutoResetEvent _are;

        private readonly object _locker;


        public SyncBuffer()
        {
            _list = new LinkedList<object>();
            _are = new AutoResetEvent(false); 
            _locker = new object();
        }

        public void insert(object o)
        {
            lock(_locker)
            {
                _list.AddLast(o);
            }

            //Wake up one thread
            _are.Set();
        }

        public object remove()
        {
            if (_list.Count == 0)
            {
                //Puts the current thread to sleep
                _are.WaitOne();
            }
            
            object r;
            
            lock (_locker)
            {
                r = _list.ElementAt(0);
                _list.RemoveFirst();
            }

            return r;
        }
    }
}
