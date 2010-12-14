using System;
using System.Collections.Generic;
using System.Collections;

using CommModule.Messages;


namespace CommModule
{
    class SessionKey
    {
        public string key;
        public DateTime validity;

        SessionKey(string k, DateTime v)
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
        private string _myPrivateKey;
        private string _myPublicKey;

        //Grabbed from the RA
        private long _refNumber;
        private string _iak;

        private Certificate _myCertificate;

        //PKI public key is trusted
        private const string _pkiPublicKey = "key here";

        private UDPSecureSocket _sendSocket;
        private UDPSecureSocket _receiveSocket;

        public string PrivateKey
        {
            get {return _myPrivateKey; }
        }

        public string getSessionKey(Node node)
        {
            if (!haveKey(node))
                generateSessionKey(node);

            SessionKey sk = (SessionKey)_sessionKeys[node];

            if (sk.validity > DateTime.Now)
            {
                generateSessionKey(node);
                sk = (SessionKey)_sessionKeys[node];
            }

            return sk.key;
        }

        public Certificate getCertificate(Node node)
        {
            if (!haveCertificate(node))
                requestCertificate(node);

            return (Certificate)_certificates[node];
        }

        public KeysManager(long refNumber, string iak, UDPSecureSocket sendSocket, UDPSecureSocket recvSocket)
        {
            _sessionKeys = new Hashtable();
            _certificates = new Hashtable();

            _sendSocket = sendSocket;
            _receiveSocket = recvSocket;

            _refNumber = refNumber;
            _iak = iak;

            generatePairOfKeys();
            getOwnCertificate(refNumber, iak);
        }
        

        private bool haveKey(Node node)
        {
            return _sessionKeys.ContainsKey(node);
        }

        private bool haveCertificate(Node node)
        {
            return _certificates.ContainsKey(node);
        }

        private void generatePairOfKeys()
        {
            _myPrivateKey = "a";
            _myPublicKey = "b";
        }
        
        private void generateSessionKey(Node node)
        {
        }

        /*
         * Request a certificate from the PKI.
         */
        private void getOwnCertificate(long refNmber, string iak)
        {
            CertificateGenerationRequest cgr = new CertificateGenerationRequest(refNmber, _myPublicKey, "127.0.0.1", 5555);
            
            //Sent in clear and signed with the IAK
            _sendSocket.sendMessageWithSpecificKey(cgr, "127.0.0.1", 2021, null, iak);


            //The certificate will be received encrypted with my own publicKey.
            //Signed with the IAK
            Certificate cert = (Certificate) _receiveSocket.receiveMessageWithSpecificKey(_myPrivateKey, _iak);
            if (cert == null)
                return;
            else
                _myCertificate = cert;
        }

        /*
         * Requests certificates from other nodes.
         */
        private void requestCertificate(Node node)
        {
        }

        private bool checkCertificate(Certificate cert)
        {
            //Verificar campos e verificar CRL
            
            return true;
        }
    }
}
