using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKI
{
    class PKI
    {
        private RA _ra;
        private CRL _crl;
        private CA _ca;

        public PKI()
        {
            _ra = new RA();
            _crl = new CRL();
            _ca = new CA();
        }

        //CLR Use Cases
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
        public long registerOffBand()
        {
            return _ra.newRegister();
        }

        public string getIAK(long reference)
        {
            return _ra.getIAK(reference);
        }


        //CA Use Cases
    }
}
