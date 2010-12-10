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

        private UDPSocket _socket;

        private object _socketLock;

        private PKI _pki;


        public WorkerThread(SyncBuffer buf, PKI pki)
        {
            _buffer = buf;
            _pki = pki;
            _socket = new UDPSocket(2020);
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

                    //Falta cifrar o certificado com a IAK e enviar
                }
                else if (objectType == "CommModule.Messages.CRLMessage")
                {
                    CRLMessage crlm = (CRLMessage) o;

                    crlm.IsRevocated = _pki.isCertificateRevocated(crlm.SerialNumber);

                    sendResponse(crlm, crlm.AdressToAnswer, crlm.PortToAnswer);
                }
            }
        }


        private void sendResponse(object message, string ip, int port)
        {
            //Only one worker thread can use the socket at a given time
            lock(_socketLock)
            {
                _socket.sendMessage(message, ip, port);
            }
        }
    }
}
