using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CommModule;

namespace Tracker
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            KeysManager keyManager = null;

            UDPSecureSocket sendingSocket = null;
            UDPSecureSocket listeningSocket = null;

            int listeningPort = 0;
            int sendingPort = 0;

            bool haveListeningSocket = false;
            bool haveSendingSocket = false;

            while (!haveListeningSocket)
            {
                Console.WriteLine("[Tracker] Which port to listen?");
                try
                {
                    listeningPort = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("[Tracker] Problem on creating socket at " + listeningPort);
                    Console.WriteLine(e.Message);
                    continue;
                }
                if (listeningPort >= 0 && listeningPort <= 65535)
                {
                    try
                    {
                        keyManager = new KeysManager(listeningPort);
                        listeningSocket = new UDPSecureSocket(listeningPort, keyManager);
                        Console.WriteLine("[Tracker] Socket created at " + listeningPort);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[Tracker] Problem on creating socket at " + listeningPort);
                        Console.WriteLine(e.Message);
                        continue;
                    }
                    haveListeningSocket = true;
                }
            }
            //Console.WriteLine("[Tracker] Which port to send?");
            try
            {
                //sendingPort = Convert.ToInt32(Console.ReadLine());
                sendingPort = listeningPort + 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("[Tracker] Problem on creating socket at " + sendingPort);
                Console.WriteLine(e.Message);
                return;
            }
            if (sendingPort >= 0 && sendingPort <= 65535)
            {
                try
                {
                    sendingSocket = new UDPSecureSocket(sendingPort, keyManager);
                    Console.WriteLine("[Tracker] Socket created at " + sendingPort);
                }
                catch (Exception e)
                {
                    Console.WriteLine("[Tracker] Problem on creating socket at " + sendingPort);
                    Console.WriteLine(e.Message);
                    Console.WriteLine("[Tracker] Please choose a different port.");
                    return;
                }
            }

            keyManager.ReceiveSocket = listeningSocket;
            keyManager.SendSocket = sendingSocket;

            Console.WriteLine("[Tracker] Sockets criados.");

            // ToDo: Pass the sockets to ThreadWorker's constructor.
            ThreadWorker tw = new ThreadWorker(listeningPort, sendingPort, listeningSocket,sendingSocket,keyManager);
            ThreadStart listener = new ThreadStart(tw.Listener);
            Thread thread = new Thread(listener);
            thread.IsBackground = true;
            thread.Start();
            Console.WriteLine("[Tracker] Worker started.");
        }
    }
}
