using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace PKI
{
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


        public long newRegister()
        {
            _refAndKeysTable[_actualRefNumber] = generateKey(128);
            _actualRefNumber++;

            return _actualRefNumber - 1;
        }

        public string getIAK(long reference)
        {
            return (string) _refAndKeysTable[reference];
        }

        /*
         * Generate a random IAK. It's dumb but works for now.
         * Size in bits.
         */
        private string generateKey(int size)
        {
            string builder = "";
            Random random = new Random();
            char ch;
            for (int i = 0; i < size / 8; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder += ch;
            }

            return builder;
        }


    }
}
