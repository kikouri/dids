using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace IDS
{
    static class IDSMain
    {
        [STAThread]
        static void Main()
        {
            IDS ids = new IDS();
            ids.Run();
        }        
    }
}
