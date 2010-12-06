﻿using System;
using System.Collections.Generic;
using System.Text;
using CommModule.Messages;
using CommModule;
using System.Net;

namespace TestaTracker
{
    public class TestaTracker
    {
        public static void Main(string[] args)
        {

            UDPSecureSocket uss = new UDPSecureSocket(1222);
            TrackerRequestMessage tr = new TrackerRequestMessage("127.0.0.1", 1222, DateTime.Now, 1222);
            uss.sendMessage(tr, "127.0.0.1", 1245);
            Console.WriteLine("Sent!");
            TrackerAnswerMessage ta = (TrackerAnswerMessage)uss.receiveMessage();
            Console.WriteLine(ta.ActiveNodeList.Count);
            Console.ReadLine();

            /*
            UDPSecureSocket uss = new UDPSecureSocket(1222);
            TestMessage tm = new TestMessage("LOL", 1);
            uss.sendMessage(tm, IPAddress.Parse("127.0.0.1"), 1245);
            Console.WriteLine("Sent!");
            Console.ReadLine();
             * */
        }
    }
}