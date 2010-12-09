using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public CA(PKI pki)
        {
            _pki = pki;
            _actualSerialNumber = 0;
        }

        public Certificate generateCertificate(long refNumber, string publicKey)
        {
            //VALIDADE ERRADA
            Certificate c = new Certificate(_actualSerialNumber, "SIRS-CA", DateTime.Now,
                _pki.getSubject(refNumber), publicKey, "SIGNATURE");

            return c;
        }
    }
}
