using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;

using CommModule;

namespace IDS
{
    public class IDS
    {
        private ArrayList _messagesToSend;
        private ArrayList _statusMessages;
        private Hashtable _receivedAttacks;
        private ArrayList _publishedAttacks;
        private ArrayList _publishedSolutions;
        private Status _idsStatus;
        private ActiveNodes _activeNodes;

        private KeysManager _keyManager;

        public IDS()
        {

            _idsStatus = new Status();
            _statusMessages = new ArrayList();
            _receivedAttacks = new Hashtable();
            _messagesToSend = new ArrayList();
            _activeNodes = new ActiveNodes();
            _publishedAttacks = new ArrayList();
            _publishedSolutions = new ArrayList();

            _keyManager = new KeysManager();
        }

        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (File.Exists("c:\\IDS\\passwd.bin"))
            {
                Application.Run(new Menus.UserLogin(_idsStatus));
            }
            else
            {
                Application.Run(new Menus.UserRegisterMenu(_idsStatus));
            }

            if (_idsStatus.IsLoggedOn)
            {
                MessageSenderThread messageSender = new MessageSenderThread(_idsStatus, ArrayList.Synchronized(_messagesToSend), _activeNodes, ArrayList.Synchronized(_publishedAttacks), Hashtable.Synchronized(_receivedAttacks), ArrayList.Synchronized(_publishedSolutions), _keyManager);
                ThreadStart messageSenderThreadStart = new ThreadStart(messageSender.Run);
                Thread messageSenderThread = new Thread(messageSenderThreadStart);
                messageSenderThread.Start();

                MessageReceiverThread messageReceiver = new MessageReceiverThread(_idsStatus, Hashtable.Synchronized(_receivedAttacks), ArrayList.Synchronized(_statusMessages), _keyManager);
                ThreadStart messageReceiverThreadStart = new ThreadStart(messageReceiver.Run);
                Thread messageReceiverThread = new Thread(messageReceiverThreadStart);
                messageReceiverThread.Start();

                StatusListenerThread statusListener = new StatusListenerThread(_idsStatus, ArrayList.Synchronized(_statusMessages), _activeNodes, ArrayList.Synchronized(_publishedAttacks), ArrayList.Synchronized(_messagesToSend), ArrayList.Synchronized(_publishedSolutions));
                ThreadStart statusListenerThreadStart = new ThreadStart(statusListener.Run);
                Thread statusListenerThread = new Thread(statusListenerThreadStart);
                statusListenerThread.Start();

                StatusSenderThread statusSender = new StatusSenderThread(_idsStatus, ArrayList.Synchronized(_messagesToSend), _activeNodes);
                ThreadStart statusSenderThreadStart = new ThreadStart(statusSender.Run);
                Thread statusSenderThread = new Thread(statusSenderThreadStart);
                statusSenderThread.Start();

                Application.EnableVisualStyles();
                Application.Run(new Menus.MainMenu(_idsStatus, ArrayList.Synchronized(_messagesToSend), Hashtable.Synchronized(_receivedAttacks)));
            }
        }        
    }
}
