using System;
using System.Collections.Generic;
using System.Text;
using CommModule.Messages;
using CommModule;
using System.Net;
using System.Collections;

namespace TestaTracker
{
    public class TestaPKI
    {
        public static void Main(string[] args)
        {
            int port = Convert.ToInt32(Console.ReadLine());
            UDPSecureSocket uss = new UDPSecureSocket(port);

            while (true)
            {
                Console.WriteLine("Serial to check:");
                int serial = Convert.ToInt32(Console.ReadLine());
                
                CRLMessage crlm = new CRLMessage(serial, "127.0.0.1", port);
                uss.sendMessage(crlm, "127.0.0.1", 2021);
                Console.WriteLine("Sent!");
                crlm = (CRLMessage) uss.receiveMessage();

                if (crlm.IsRevocated)
                {
                    Console.WriteLine("Yes it is revocated!!");
                }
                else
                {
                    Console.WriteLine("No it is not revocated!!");
                }
            }
        }
    }
}
