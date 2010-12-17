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

        //Both keys of the PKI are hardcoded
        private const string _keyPair = "<RSAKeyValue><Modulus>tVK7VUqocxn91PndZIVi8U65mggrNt24AnkbZwlEn+4rsZc6oWxT84Ffyx08XK0seBBMdPey2wIaFkWj+lsvLnK1W991dNezeh4MIRnh/8Kr0rvPDRZjX1fIau0qkOrlcWRJdAppUW4jo/8wjlMOASkqtNjyWPj6XcT8QmcKcL8=</Modulus><Exponent>AQAB</Exponent><P>6XXmdtJgc5Mg9436jy14D9dhS6Edzz9rBMKuv+s1qWgIxfqO8ef89kUIQBkoJVxllwy8odbX8YGF+66fX2WWHQ==</P><Q>xtQ6m4Sfao19TaGvfN8c3dqXB042jXdx4hz0MggBFCj1RNEHUzxdF7QiuceWnIxnzP+Kd+vMz6ud1yfJ5yp7iw==</Q><DP>YSO6ijRNB6nvbLH50Htl2omOpU5bvfEwUWHEHnz67gsoo1/2/Ha/zaS5oxoUlz8T0j7tehWP8qAnJKrrC2GUjQ==</DP><DQ>bu0qgWc9VUH43V3OPRlwzmlMhzvgfY5dD+xdZKhIicnMBIel0Y9E1JugIAu1AEPpCVqsEvmP+3BgkA/Xuctevw==</DQ><InverseQ>Fazs4v/aMMtmTb1JvRS5vyUErGM+ytBDbO28ykiOokza7bd3SBLmUMZLcwANvcaf3zIfNMCDzUkKp1hLrSGhuw==</InverseQ><D>eEXDSwT1jTutWirPBpFPmv58MEbA22jgWIuaeJL2ORtTUj4cvtxLJ1cmgmUHF/YqbA1rmZ5/vbO8OA/DgBohxY/EbsGGNVMibb+AJVxAttdET/KGi1JlsjRQw+3HvoIY5VZ0HblvUKMyKQoz8Vu2kIts7qEFdIYbFBehsU9QvGk=</D></RSAKeyValue>";
        public string KeyPair
        {
            get { return _keyPair; }
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

        public void deleteRegister(long reference)
        {
            _ra.deleteRegister(reference);
        }


        //CA Use Cases

        public Certificate generateCertificate(long referenceNumber, string publicKey)
        {
            return _ca.generateCertificate(referenceNumber, publicKey);
        }
    }
}
