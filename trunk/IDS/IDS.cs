using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace IDS
{
    class IDS
    {
        private ArrayList _messagesToSend;
        private ArrayList _statusMessages;
        private ArrayList _receivedAttacks;

        public IDS() { }

        public void Run()
        {
            MessageSenderThread messageSender = new MessageSenderThread();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menus.MainMenu());
        }

        public ArrayList MessagesToSend
        {
            get { return _messagesToSend; }
            set { _messagesToSend = value; }
        }

        public ArrayList StatusMessages
        {
            get { return _statusMessages; }
            set { _statusMessages = value; }
        }

        public ArrayList ReceivedAttacks
        {
            get { return _receivedAttacks; }
            set { _receivedAttacks = value; }
        }
    }
}
