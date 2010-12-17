using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using System.Net;
using System.Security.Cryptography;

using CommModule.Messages;


namespace CommModule
{
    class SessionKey
    {
        public string key;
        public DateTime validity;

        public SessionKey(string k, DateTime v)
        {
            key = k;
            validity = v;
        }
    }
    
    public class KeysManager
    {
        //Maps nodes to its session keys
        private Hashtable _sessionKeys;

        //Maps nodes to its certificates
        private Hashtable _certificates;

        //The keys of the entity who owns this object
        private string _myPrivateAndPublicKeys;

        //Only the public key
        private string _myPublicKey;

        private UDPSecureSocket _receiveSocket;
        private UDPSecureSocket _sendSocket;

        private int _receivingPort;
        private String _receivingAddress;

        //Grabbed from the RA
        private long _refNumber;
        private string _iak;

        private Certificate _myCertificate;

        //PKI public key is trusted
        private const string _pkiPublicKey = "<RSAKeyValue><Modulus>tVK7VUqocxn91PndZIVi8U65mggrNt24AnkbZwlEn+4rsZc6oWxT84Ffyx08XK0seBBMdPey2wIaFkWj+lsvLnK1W991dNezeh4MIRnh/8Kr0rvPDRZjX1fIau0qkOrlcWRJdAppUW4jo/8wjlMOASkqtNjyWPj6XcT8QmcKcL8=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private string _pkiAddress;

        //In minutes
        private const int _sessionKeysValidity = 10;
        
        public string PrivateAndPublicKeys
        {
            get {return _myPrivateAndPublicKeys; }
        }

        public string PKIPublicKey
        {
            get { return _pkiPublicKey; }
        }

        public UDPSecureSocket ReceiveSocket
        {
            set 
            {
                _receiveSocket = value;

                //If it has both sockets start
                if (_sendSocket != null)
                    start();
            }
        }

        public UDPSecureSocket SendSocket
        {
            set 
            { 
                _sendSocket = value;

                //If it has both sockets start
                if (_receiveSocket != null)
                    start();
            }
        }

        public Certificate MyCertificate
        {
            get { return _myCertificate; }
        }

        public string PKIAddress
        {
            get { return _pkiAddress; }
        }

        public string ReceivingAddress
        {
            get { return _receivingAddress; }
        }

        public KeysManager(int receivingPort)
        {
            _sessionKeys = new Hashtable();
            _certificates = new Hashtable();

            _refNumber = -1;
            _iak = null;

            _sendSocket = null;
            _receiveSocket = null;

            _receivingPort = receivingPort;


            //Get the local network IP
            IPAddress[] hostIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress hostIP in hostIPs)
            {
                if (hostIP.ToString().StartsWith("192.168.") ||
                    hostIP.ToString().StartsWith("10.") ||
                    hostIP.ToString().StartsWith("172.16"))
                    _receivingAddress = hostIP.ToString();
            }
        }

        public void start()
        {
            generatePairOfKeys();

            RefNumAndIAK inputBox = new RefNumAndIAK();
            inputBox.ShowDialog();
            _refNumber = inputBox.ReferenceNumber;
            _iak = inputBox.IAK;
            _pkiAddress = inputBox.PKIAddress;

            getOwnCertificate(_refNumber, _iak);
        }

        public string getSessionKey(string add, int recvPort, int sendPort)
        {
            if (!haveKey(add, recvPort, sendPort))
                generateSessionKey(add, recvPort, sendPort);

            SessionKey sk = (SessionKey)_sessionKeys[add+recvPort+sendPort];

            if (sk.validity < DateTime.Now)
            {
                Console.WriteLine("[CommLayer] The Session Key to " + add + ":" + recvPort + ":" + sendPort + " expired.");
                generateSessionKey(add, recvPort, sendPort);
                sk = (SessionKey)_sessionKeys[add + recvPort + sendPort];
            }

            return sk.key;
        }

        public Certificate getCertificate(string add, int recvPort, int sendPort)
        {
            if (!haveCertificate(add, recvPort, sendPort))
                requestCertificate(add, recvPort, sendPort);

            Certificate c = (Certificate)_certificates[add + recvPort + sendPort];

            if (! checkCertificate(c))
                requestCertificate(add, recvPort, sendPort);

            return c;
        }



        private bool haveKey(string add, int recvPort, int sendPort)
        {
            return _sessionKeys.Contains(add+recvPort+sendPort);
        }

        private bool haveCertificate(string add, int recvPort, int sendPort)
        {
            return _certificates.Contains(add+recvPort+sendPort);
        }


        private void generatePairOfKeys()
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            _myPrivateAndPublicKeys = RSA.ToXmlString(true);

            _myPublicKey = RSA.ToXmlString(false);
        }

        private void generateSessionKey(string add, int recvPort, int sendPort)
        {
            Console.WriteLine("[CommLayer] Generating Session Key to node: " + add + ":" + recvPort + ":" + sendPort);
            
            SessionKey sk = new SessionKey(Cryptography.generateAESKey(), DateTime.Now.AddMinutes(_sessionKeysValidity));
            SessionKeyMessage skm = new SessionKeyMessage(sk.key, sk.validity, _receivingAddress, _receivingPort);

            Certificate nodeCertificate = getCertificate(add,recvPort,sendPort);

            _sendSocket.sendMessageWithSpecificKey(skm, add, recvPort, nodeCertificate.SubjectPublicKey, _myPrivateAndPublicKeys, "RSA", "RSA");

            _receiveSocket.Bypass = true;
            SessionKeyMessageACK skma = (SessionKeyMessageACK)_receiveSocket.receiveMessage();
            _receiveSocket.Bypass = false;

            _sessionKeys[add + recvPort + sendPort] = sk;

            Console.WriteLine("[CommLayer] Session Key sent and accepted");
        }

        /*
         * Request a certificate from the PKI.
         */
        private void getOwnCertificate(long refNmber, string iak)
        {
            Console.WriteLine("[CommLayer] Getting a certificate for my public key.");
            
            CertificateGenerationRequest cgr = new CertificateGenerationRequest(refNmber, _myPublicKey, _receivingAddress, _receivingPort);
            
            //Sent in clear and signed with the IAK
            _sendSocket.sendMessageWithSpecificKey(cgr, _pkiAddress, 2021, null, iak, "AES", "AES");

            //The certificate will be received encrypted with my own publicKey.
            //Signed with the PKI private key
            Certificate cert = (Certificate)_receiveSocket.receiveMessageWithSpecificKey(_myPrivateAndPublicKeys, _pkiPublicKey, "RSA", "RSA");
            if (cert == null)
            {
                return;
            }
            else
            {
                cert.SubjectAddress = _receivingAddress;
                _myCertificate = cert;
                Console.WriteLine("[CommLayer] I Got my certificate.");
            }
        }

        /*
         * Requests certificates from other nodes.
         */
        private void requestCertificate(string add, int recvPort, int sendPort)
        {
            Console.WriteLine("[CommLayer] Requesting certificate to node: " + add + ":" + recvPort + ":" + sendPort);
            
            CertificateRequestMessage crm = new CertificateRequestMessage(_receivingAddress, _receivingPort, _myCertificate);

            _sendSocket.Bypass = true;
            _sendSocket.sendMessage(crm, add, recvPort);
            _sendSocket.Bypass = false;

            //Waits for the certificate to arrive
            System.Threading.Thread.Sleep(500);

            _receiveSocket.Bypass = true;
            Certificate c = (Certificate)_receiveSocket.receiveMessage();
            _receiveSocket.Bypass = false;

            _certificates[add+recvPort+sendPort] = c;
        }

        private bool checkCertificate(Certificate cert)
        {
            Console.WriteLine("[CommLayer] Checking certificate fields");
            
            if (cert.Issuer != "SIRS-CA")
                return false;

            //Como verificar isto??
            //if (cert.Subject == "?????")
              //  return false;

            if (cert.Validity < DateTime.Now)
                return false;

            if (! Cryptography.checkCertificateSignature(cert, _pkiPublicKey))
                return false;

            Console.WriteLine("[CommLayer] Checking with the CRL");

            CRLMessage crl = new CRLMessage(cert.SerialNumber, _receivingAddress, _receivingPort);

            _sendSocket.Bypass = true;
            _sendSocket.sendMessage(crl, _receivingAddress, 2021);
            _sendSocket.Bypass = false;

            _receiveSocket.Bypass = true;
            crl = (CRLMessage)_receiveSocket.receiveMessage();
            _receiveSocket.Bypass = false;

            if (crl.IsRevocated)
                return false;

            Console.WriteLine("[CommLayer] The certificate is valid.");
            return true;
        }

        public void addSessionKey(string address, int receivePort, int sendPort, string key, DateTime dt)
        {
            SessionKey sk = new SessionKey(key, dt);

            _sessionKeys[address + receivePort + sendPort] = sk;
        }

        public void addCertificate(string address, int receivePort, int sendPort, Certificate c)
        {
            _certificates[address + receivePort + sendPort] = c;
        }
    }
}
