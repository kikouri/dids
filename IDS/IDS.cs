using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;

namespace IDS
{
    class IDS
    {
        private ArrayList _messagesToSend;
        private ArrayList _statusMessages;
        private ArrayList _receivedAttacks;
        private Status _idsStatus;
        private ActiveNodes _activeNodes;

        public IDS()
        {
            _idsStatus = new Status();
            _statusMessages = new ArrayList();
            _receivedAttacks = new ArrayList();
            _messagesToSend = new ArrayList();
            _activeNodes = new ActiveNodes();
        }

        public void Run()
        {
            MessageSenderThread messageSender = new MessageSenderThread(_idsStatus, ArrayList.Synchronized(_messagesToSend), _activeNodes);
            ThreadStart messageSenderThreadStart = new ThreadStart(messageSender.Run);
            Thread messageSenderThread = new Thread(messageSenderThreadStart);
            messageSenderThread.Start();

            MessageReceiverThread messageReceiver = new MessageReceiverThread(_idsStatus, ArrayList.Synchronized(_receivedAttacks), ArrayList.Synchronized(_statusMessages));
            ThreadStart messageReceiverThreadStart = new ThreadStart(messageReceiver.Run);
            Thread messageReceiverThread = new Thread(messageReceiverThreadStart);
            messageReceiverThread.Start();

            StatusListenerThread statusListener = new StatusListenerThread(_idsStatus, ArrayList.Synchronized(_statusMessages), _activeNodes);
            ThreadStart statusListenerThreadStart = new ThreadStart(statusListener.Run);
            Thread statusListenerThread = new Thread(statusListenerThreadStart);
            statusListenerThread.Start();

            StatusSenderThread statusSender = new StatusSenderThread(_idsStatus, ArrayList.Synchronized(_messagesToSend), _activeNodes);
            ThreadStart statusSenderThreadStart = new ThreadStart(statusSender.Run);
            Thread statusSenderThread = new Thread(statusSenderThreadStart);
            statusSenderThread.Start();
            
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
