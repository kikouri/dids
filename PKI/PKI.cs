using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using CommModule.Messages;
using CommModule;

namespace PKI
{
    public class PKI
    {
        private RA _ra;
        private CRL _crl;
        private CA _ca;

        private SyncBuffer _receivedMessagesBuffer;

        private const string _privateKey = "key here";
        public string PrivateKey
        {
            get { return _privateKey; }
        }

        public PKI()
        {
            _ra = new RA();
            _crl = new CRL();
            _ca = new CA(this);

            _receivedMessagesBuffer = new SyncBuffer();
        }

        public void Run()
        {
            MessageReceiverThread messageReceiver = new MessageReceiverThread(_receivedMessagesBuffer, 2021);
            Thread messageReceiverThread = new Thread(messageReceiver.Run);
            messageReceiverThread.IsBackground = true;
            messageReceiverThread.Start();

            WorkerThread worker = new WorkerThread(_receivedMessagesBuffer, this);
            Thread workerThread = new Thread(worker.Run);
            workerThread.IsBackground = true;
            workerThread.Start();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI(this));
        }

        //CRL Use Cases
        public void revocateCertificate(long serial)
        {
            _crl.revocateCertificate(serial);
        }

        public bool isCertificateRevocated(long serial)
        {
            return _crl.isCertificateRevocated(serial);
        }

        public LinkedList<long> getRevocationList()
        {
            return _crl.RevocationList;
        }


        //RA Use Cases
        public long registerOffBand(string subject)
        {
            return _ra.newRegister(subject);
        }

        public string getIAK(long reference)
        {
            return _ra.getIAK(reference);
        }

        public string getSubject(long reference)
        {
            return _ra.getSubject(reference);
        }

        public bool isReferenceValid(long reference)
        {
            return _ra.isReferenceValid(reference);
        }


        //CA Use Cases

        public Certificate generateCertificate(long referenceNumber, string publicKey)
        {
            return _ca.generateCertificate(referenceNumber, publicKey);
        }
    }
}
