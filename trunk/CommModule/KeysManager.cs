using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;

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

        private UDPSecureSocket _receiveSocket;
        private UDPSecureSocket _sendSocket;

        //Grabbed from the RA
        private long _refNumber;
        private string _iak;

        private Certificate _myCertificate;

        //PKI public key is trusted
        private const string _pkiPublicKey = "key here";

        public string PrivateKey
        {
            get {return _myPrivateKey; }
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


        public KeysManager()
        {
            _sessionKeys = new Hashtable();
            _certificates = new Hashtable();

            _refNumber = -1;
            _iak = null;

            _sendSocket = null;
            _receiveSocket = null;
        }

        public void start()
        {
            generatePairOfKeys();

            RefNumAndIAK inputBox = new RefNumAndIAK();
            inputBox.ShowDialog();
            _refNumber = inputBox.ReferenceNumber;
            _iak = inputBox.IAK;

            getOwnCertificate(_refNumber, _iak);
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

        

        private bool haveKey(Node node)
        {
            return _sessionKeys.ContainsKey(node);
        }

        private bool haveCertificate(Node node)
        {
            return _certificates.ContainsKey(node);
        }

        /*
         * 
         */
        private void generatePairOfKeys()
        {
            _myPrivateKey = "a";
            _myPublicKey = "b";
        }
        
        private void generateSessionKey(Node node)
        {
            //generate a aes key and send it to the node encripted with kp
        }

        /*
         * Request a certificate from the PKI.
         * 
         * TODO: CHANGE IP AND PORT
         */
        private void getOwnCertificate(long refNmber, string iak)
        {                       
            CertificateGenerationRequest cgr = new CertificateGenerationRequest(refNmber, _myPublicKey, "127.0.0.1", 2040);
            
            //Sent in clear and signed with the IAK
            _sendSocket.sendMessageWithSpecificKey(cgr, "127.0.0.1", 2021, null, iak);


            //The certificate will be received encrypted with my own publicKey.
            //Signed with the IAK
            Certificate cert = (Certificate)_receiveSocket.receiveMessageWithSpecificKey(_myPrivateKey, _iak);
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
