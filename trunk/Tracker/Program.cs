﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CommModule;

namespace Tracker
{
    class Program
    {
        static void Main(string[] args)
        {

            KeysManager keyManager = new KeysManager();

            UDPSecureSocket sendingSocket = null;
            UDPSecureSocket listeningSocket = null;

            int listeningPort = 0;
            int sendingPort = 0;

            bool haveListeningSocket = false;
            bool haveSendingSocket = false;

            while (!haveListeningSocket)
            {
                Console.WriteLine("[Tracker]Which port to listen?");
                try
                {
                    listeningPort = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("[Tracker] Please insert again.");
                    continue;
                }
                if (listeningPort >= 0 && listeningPort <= 65535)
                {
                    try
                    {
                        listeningSocket = new UDPSecureSocket(listeningPort, keyManager);
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

            while (!haveSendingSocket)
            {
                Console.WriteLine("[Tracker]Which port to send?");
                sendingPort = Convert.ToInt32(Console.ReadLine());
                if (sendingPort >= 0 && sendingPort <= 65535)
                {
                    try
                    {
                        sendingSocket = new UDPSecureSocket(sendingPort, keyManager);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[Tracker] Problem on creating socket at " + sendingPort);
                        continue;
                    }
                    haveSendingSocket = true;
                }
            }

            keyManager.ReceiveSocket = listeningSocket;
            keyManager.SendSocket = sendingSocket;
            Console.WriteLine("[Tracker] Sockets criados.");

            // ToDo: Pass the sockets to ThreadWorker's constructor.
            ThreadWorker tw = new ThreadWorker(listeningPort, sendingPort, listeningSocket,sendingSocket,keyManager);
            ThreadStart listener = new ThreadStart(tw.Listener);
            Thread thread = new Thread(listener);
            thread.Start();
            Console.WriteLine("[Tracker] Worker started.");
        }
    }
}