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
            ThreadWorker tw = new ThreadWorker(1245);
            ThreadStart listener = new ThreadStart(tw.Listener);
            Thread thread = new Thread(listener);
            thread.Start();
            Console.WriteLine("[Tracker] Worker started.");
        }
    }
}
