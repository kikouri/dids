using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommModule;
using CommModule.Messages;

namespace PKI
{
    /*
     * Certification Authority
     * 
     * Generates certificates by request of end entities.
     * The requests must be identified by the reference number and authenticated with the IAK, 
     * both retrieved from the RA.
     */
    class CA
    {
        private PKI _pki;

        private long _actualSerialNumber;

        //In minutes
        private const double _certificateValidity = 30;

        public CA(PKI pki)
        {
            _pki = pki;
            _actualSerialNumber = 0;
        }

        public Certificate generateCertificate(long refNumber, string publicKey)
        {
            Certificate c;

            if (_pki.isReferenceValid(refNumber))
            {
                c = new Certificate(_actualSerialNumber, "SIRS-CA", DateTime.Now.AddMinutes(_certificateValidity),
                    _pki.getSubject(refNumber), publicKey);

                c.sign(_pki.KeyPair);
         

                _actualSerialNumber++;

                _pki.deleteRegister(refNumber);

                return c;
            }
            else
            {
                return null;
            }
        }
    }
}
