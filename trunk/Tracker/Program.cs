using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace Tracker
{
    class Program
    {
        static void Main(string[] args)
        {

            int port1 = 1245;

            ThreadWorker tw = new ThreadWorker(port1);
            ThreadStart listener = new ThreadStart(tw.Listener);
            Thread thread = new Thread(listener);
            thread.Start();
            Console.WriteLine("[Tracker] Worker started.");
        }
    }
}
