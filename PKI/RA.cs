using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

using CommModule;

namespace PKI
{
    class RegisterNode
    {
        public string IAK;
        public string subject;
        public bool isUsed;
    }
    
        
    /*
     * Registration Authority
     * 
     * Assumes a off band register of an entity within it.
     * From the register the entity gets a reference number and an Initial Authentication Key (IAK).
     * From there the entiy can do online requests of certificate generation.
     */
    public class RA
    {
        private long _actualRefNumber;

        private Hashtable _refAndKeysTable;


        public RA()
        {
            _actualRefNumber = 0;
            _refAndKeysTable = new Hashtable();
        }


        public long newRegister(string subject)
        {
            RegisterNode rn = new RegisterNode();
            rn.IAK = generateKey();
            rn.subject = subject;
            rn.isUsed = false;
            
            _refAndKeysTable[_actualRefNumber] = rn;
            _actualRefNumber++;

            return _actualRefNumber - 1;
        }

        public void deleteRegister(long refNum)
        {
            RegisterNode rm = (RegisterNode) _refAndKeysTable[refNum];
            rm.isUsed = true;
        }

        public string getIAK(long reference)
        {
            RegisterNode rn = (RegisterNode) _refAndKeysTable[reference];
            return rn.IAK;
        }

        public string getSubject(long reference)
        {
            RegisterNode rn = (RegisterNode) _refAndKeysTable[reference];
            return rn.subject;
        }

        public bool isReferenceValid(long reference)
        {
            RegisterNode rn = (RegisterNode) _refAndKeysTable[reference];
            if(rn.isUsed || reference > _actualRefNumber)
                return false;
            else
                return true;
        }

        /*
         * Generate a random IAK, to use in AES.
         */
        private string generateKey()
        {
            return Cryptography.generateAESKey();
        }
    }
}
