using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Security;

using CommModule;
using CommModule.Messages;

namespace IDS
{
    public class IDS
    {
        private ArrayList _messagesToSend;
        private ArrayList _statusMessages;
        private Hashtable _receivedAttacks;
        private ArrayList _receivedSolutions;
        private ArrayList _publishedAttacks;
        private ArrayList _publishedSolutions;
        private Status _idsStatus;
        private ActiveNodes _activeNodes;

        private KeysManager _keyManager;

        public IDS()
        {

            _idsStatus = new Status();
            _statusMessages = ArrayList.Synchronized(new ArrayList());
            _receivedAttacks = Hashtable.Synchronized(new Hashtable());
            _receivedSolutions = ArrayList.Synchronized(new ArrayList());
            _messagesToSend = ArrayList.Synchronized(new ArrayList());
            _activeNodes = new ActiveNodes();
            _publishedAttacks = ArrayList.Synchronized(new ArrayList());
            _publishedSolutions = ArrayList.Synchronized(new ArrayList());

            _keyManager = null;
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

            if (File.Exists("c:\\IDS\\Trackers.txt"))
            {
                GetTrackersDetails();
            }
            else
            {
                MessageBox.Show("The application could not start without the trackers file!!", "Tracker File Missing");
                return;
            }

            if (_idsStatus.IsLoggedOn)
            {
                if (File.Exists("c:\\IDS\\IdsData.bin"))
                    DeserializeApplicationContent();

                Console.WriteLine("Port to receive");
                int portReceive = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Port to send");
                int portSend = Int32.Parse(Console.ReadLine());

                _keyManager = new KeysManager(portReceive);

                MessageSenderThread messageSender = new MessageSenderThread(_idsStatus, _messagesToSend, _activeNodes, _publishedAttacks, _receivedAttacks, _publishedSolutions, _keyManager, portSend);
                ThreadStart messageSenderThreadStart = new ThreadStart(messageSender.Run);
                Thread messageSenderThread = new Thread(messageSenderThreadStart);
                messageSenderThread.Start();

                MessageReceiverThread messageReceiver = new MessageReceiverThread(_idsStatus, _receivedAttacks, _statusMessages,_receivedSolutions, _keyManager, portReceive);
                ThreadStart messageReceiverThreadStart = new ThreadStart(messageReceiver.Run);
                Thread messageReceiverThread = new Thread(messageReceiverThreadStart);
                messageReceiverThread.Start();

                StatusListenerThread statusListener = new StatusListenerThread(_idsStatus, _statusMessages, _activeNodes, _publishedAttacks, _messagesToSend, _publishedSolutions);
                ThreadStart statusListenerThreadStart = new ThreadStart(statusListener.Run);
                Thread statusListenerThread = new Thread(statusListenerThreadStart);
                statusListenerThread.Start();

                StatusSenderThread statusSender = new StatusSenderThread(_idsStatus, _messagesToSend, _activeNodes);
                ThreadStart statusSenderThreadStart = new ThreadStart(statusSender.Run);
                Thread statusSenderThread = new Thread(statusSenderThreadStart);
                statusSenderThread.Start();

                Application.EnableVisualStyles();
                Application.Run(new Menus.MainMenu(_idsStatus, _messagesToSend, _receivedAttacks, _receivedSolutions));

                SerializeApplicationContent();
                _idsStatus.ErasePassword();
            }            
        }

        private void DeserializeApplicationContent()
        {
            try
            {
                File.Decrypt("c:\\IDS\\IdsData.bin");

                TextReader contentFileReader = new StreamReader("c:\\IDS\\IdsData.bin");
                String contentCipheredString = contentFileReader.ReadLine();
                contentFileReader.Close();

                String contentString = Cryptography.decryptMessageAES(contentCipheredString,
                    PasswordFile.getContentCipheringKey(_idsStatus.Password));

                MemoryStream applicationContentStream = new MemoryStream();
                BinaryFormatter deserializer = new BinaryFormatter();
                applicationContentStream.Write(System.Convert.FromBase64String(contentString), 0, System.Convert.FromBase64String(contentString).Length);
                applicationContentStream.Seek(0, SeekOrigin.Begin);

                ApplicationContent applicationContent = (ApplicationContent)deserializer.Deserialize(applicationContentStream);
                applicationContentStream.Close();          

                _publishedSolutions = ArrayList.Synchronized(applicationContent.PublishedSolutions);
                _receivedAttacks = Hashtable.Synchronized(applicationContent.ReceivedAttacks);
                _publishedAttacks = ArrayList.Synchronized(applicationContent.PublishedAttacks);
                _receivedSolutions = ArrayList.Synchronized(applicationContent.ReceivedSolutions);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (PathTooLongException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (SecurityException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void SerializeApplicationContent()
        {
            try
            {
                ApplicationContent content = new ApplicationContent();
                content.ReceivedAttacks = _receivedAttacks;
                content.PublishedAttacks = _publishedAttacks;
                content.PublishedSolutions = _publishedSolutions;
                content.ReceivedSolutions = _receivedSolutions;

                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream applicationContentStream = new MemoryStream();
                bf.Serialize(applicationContentStream, content);
                byte[] applicationContentBytes = new byte[applicationContentStream.Length];
                applicationContentBytes = applicationContentStream.ToArray();
                String applicationContentString = System.Convert.ToBase64String(applicationContentBytes);
                
                String contentCipheredString = Cryptography.encryptMessageAES(applicationContentString,
                    PasswordFile.getContentCipheringKey(_idsStatus.Password));

                TextWriter contentFileWriter = new StreamWriter("c:\\IDS\\IdsData.bin");
                contentFileWriter.WriteLine(contentCipheredString);
                contentFileWriter.Close();

                File.Encrypt("c:\\IDS\\IdsData.bin");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (PathTooLongException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (SecurityException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void GetTrackersDetails()
        {
            TextReader trackersFileReader = new StreamReader("c:\\IDS\\Trackers.txt");
            String firstTrackerAddr = trackersFileReader.ReadLine();
            String firstTrackerPort = trackersFileReader.ReadLine();
            String secondTrackerAddr = trackersFileReader.ReadLine();
            String secondTrackerPort = trackersFileReader.ReadLine();
            trackersFileReader.Close();

            _idsStatus.FirstTrackerAddr = firstTrackerAddr;
            _idsStatus.FirstTrackerPort = Int32.Parse(firstTrackerPort);
            _idsStatus.SecondTrackerAddr = secondTrackerAddr;
            _idsStatus.SecondTrackerPort = Int32.Parse(secondTrackerPort);
        }
    }
}
