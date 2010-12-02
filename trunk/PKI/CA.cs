using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public CA(PKI pki)
        {
            _pki = pki;
        }

        public void generateCertificate(long refNumber)
        {
            string IAK = _pki.getIAK(refNumber);


        }
    }
}
