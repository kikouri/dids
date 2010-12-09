using System;

namespace PKI
{
    static class PKIMain
    {
        [STAThread]
        static void Main()
        {
            PKI pki = new PKI();
            pki.Run();
        }
    }
}
