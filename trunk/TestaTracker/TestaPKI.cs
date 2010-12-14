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
            Console.WriteLine("Enter port:");
            int port = Convert.ToInt32(Console.ReadLine());
            UDPSecureSocket uss = new UDPSecureSocket(port, null);
  
            while (true)
            {
                Console.WriteLine("Certificate generation test");
                
                Console.WriteLine("Enter ref number:");
                long refN = Convert.ToInt64(Console.ReadLine());

                Console.WriteLine("Enter IAK:");
                string iak = Console.ReadLine();

                Console.WriteLine(Cryptography.decryptMessageAES(Cryptography.encryptMessageAES("Teste de cifra, se der para ler tudo OK!!!", iak), iak));
                                
                CertificateGenerationRequest cgr = new CertificateGenerationRequest(refN, iak, "127.0.0.1", port);
                uss.sendMessageWithSpecificKey(cgr, "127.0.0.1", 2021, null, iak);
                Console.WriteLine("Sent!");

                Certificate cert = (Certificate) uss.receiveMessageWithSpecificKey(iak, );
                if(cert == null)
                    Console.WriteLine("Are you an evil attacker?");
                else
                    Console.WriteLine(cert.toString());
            }
        }
    }
}
