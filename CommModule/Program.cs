using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommModule.Messages;


namespace CommModule
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            int arg = Int32.Parse(args[0]);
            if (arg == 1)
            {
                UDPSecureSocket socket = new UDPSecureSocket(2020);
                Console.ReadLine();
                TestMessage test = new TestMessage("1234", 5678);
                socket.sendMessage(test, "127.0.0.1");
                Console.WriteLine("Mensagem enviada");
                Console.ReadLine();
            }
            else
            {
                UDPSecureSocket socket = new UDPSecureSocket(2021);
                TestMessage test2 = (TestMessage)socket.receiveMessage();
                Console.WriteLine(test2.IdTest);
                Console.WriteLine(test2.NumTest);
                Console.ReadLine();
            }
            
            /*TestMessage test = new TestMessage("1234", 5678);
            byte[] testBytes = ObjectSerialization.SerializeObject(test);
            TestMessage test2 = (TestMessage)ObjectSerialization.DeserializeObject(testBytes);

            Console.WriteLine(test2.IdTest);
            Console.WriteLine(test2.NumTest);
            Console.ReadLine();
             * */
        }
    }
}
