using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKI
{
    /*
     * Certificate Revocation List
     * 
     * Maintains a list of all the revocated certificates. Answer to certificate check requests.
     */
    class CRL
    {
        private LinkedList<long> _revocationList;

        public CRL()
        {
            _revocationList = new LinkedList<long>();
        }

        public void revocateCertificate(long serialNumber)
        {
            if(! _revocationList.Contains(serialNumber))
                _revocationList.AddLast(serialNumber);
        }

        public bool isCertificateRevocated(long serialNumber)
        {
            return _revocationList.Contains(serialNumber);
        }

        public LinkedList<long> RevocationList
        {
            get { return _revocationList; }
        }
    }
}
