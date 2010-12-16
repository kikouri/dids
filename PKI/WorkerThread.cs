using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CommModule;
using CommModule.Messages;

namespace PKI
{
    public class WorkerThread
    {
        private SyncBuffer _buffer;

        private UDPSecureSocket _socket;

        private PKI _pki;


        public WorkerThread(SyncBuffer buf, PKI pki)
        {
            _buffer = buf;
            _pki = pki;
            _socket = new UDPSecureSocket(2020, null); //Don't need a KeyManager
        }

        public void Run()
        {
            GenericMessage gm;
            
            while (true)
            {
                gm = (GenericMessage) _buffer.remove();

                if (gm.ObjectType == "CommModule.Messages.CertificateGenerationRequest")
                {
                    Console.WriteLine("[PKI] Receiving Certificate Generation Request.");
                    
                    CertificateGenerationRequest cgr = (CertificateGenerationRequest) 
                        ObjectSerialization.DeserializeGenericMessage(gm);

                    if (Cryptography.checkMessageSignatureAES(gm, _pki.getIAK(cgr.ReferenceNumber)) == true)
                    {
                        Certificate cert = _pki.generateCertificate(cgr.ReferenceNumber, cgr.PublicKey);
                        Console.WriteLine("[PKI] Sending to " + cgr.AdressToAnswer + ":" + cgr.PortToAnswer);
                        _socket.sendMessageWithSpecificKey(cert, cgr.AdressToAnswer, cgr.PortToAnswer, cgr.PublicKey, _pki.KeyPair, "RSA");
                        Console.WriteLine("[PKI] Certificate Generated and sent.");
                    }
                    else
                        Console.WriteLine("[PKI] Request was malformed, no certificate sent.");
                }
                else if (gm.ObjectType == "CommModule.Messages.CRLMessage")
                {
                    Console.WriteLine("[PKI] Receiving CRL Request.");
                    
                    CRLMessage crlm = (CRLMessage) ObjectSerialization.DeserializeGenericMessage(gm);

                    crlm.IsRevocated = _pki.isCertificateRevocated(crlm.SerialNumber);

                    _socket.sendMessageWithSpecificKey(crlm, crlm.AdressToAnswer, crlm.PortToAnswer, null, null, "AES");

                    Console.WriteLine("[PKI] Response sent.");
                }
            }
        }
    }
}
