using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using CommModule.Messages;

namespace PKI
{
    public class PKI
    {
        private RA _ra;
        private CRL _crl;
        private CA _ca;

        private SyncBuffer _receivedMessagesBuffer;

        private MessageReceiverThread _messageReceiverThread;

        public PKI()
        {
            _ra = new RA();
            _crl = new CRL();
            _ca = new CA(this);

            _receivedMessagesBuffer = new SyncBuffer();
        }

        public void Run()
        {
            _messageReceiverThread = new MessageReceiverThread(_receivedMessagesBuffer, 2050);
            Thread messageReceiverThread = new Thread(_messageReceiverThread.Run);
            messageReceiverThread.Start();
            
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


        //CA Use Cases

        public Certificate generateCertificate(long referenceKey, string publicKey)
        {
            return _ca.generateCertificate(referenceKey, publicKey);
        }
    }
}
