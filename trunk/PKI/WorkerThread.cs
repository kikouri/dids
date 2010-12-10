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

        private object _socketLock;

        private PKI _pki;


        public WorkerThread(SyncBuffer buf, PKI pki)
        {
            _buffer = buf;
            _pki = pki;
            _socket = new UDPSecureSocket(2020);
            _socketLock = new object();
        }

        public void Run()
        {
            Object o;
            String objectType;
            
            while (true)
            {
                o = _buffer.remove();
                objectType = o.GetType().ToString();

                if (objectType == "CommModule.Messages.CertificateGenerationRequest")
                {
                    CertificateGenerationRequest cgr = (CertificateGenerationRequest) o;

                    Certificate cert = _pki.generateCertificate(cgr.ReferenceNumber, cgr.PublicKey);

                    if(cert != null)
                        _socket.sendMessageWithSpecificKey(cert, cgr.AdressToAnswer, cgr.PortToAnswer, _pki.getIAK(cgr.ReferenceNumber));
                }
                else if (objectType == "CommModule.Messages.CRLMessage")
                {
                    CRLMessage crlm = (CRLMessage) o;

                    crlm.IsRevocated = _pki.isCertificateRevocated(crlm.SerialNumber);

                    _socket.sendMessage(crlm, crlm.AdressToAnswer, crlm.PortToAnswer);
                }
            }
        }
    }
}
