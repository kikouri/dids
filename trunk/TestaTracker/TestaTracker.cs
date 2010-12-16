using System;
using System.Collections.Generic;
using System.Text;
using CommModule.Messages;
using CommModule;
using System.Net;
using System.Collections;

namespace TestaTracker
{
    public class TestaTracker
    {
        /*
         * Exemplo de como fazer pedidos ao tracker e o que fazer com dados retornados.
         * 
         */
        public static void Main(string[] args)
        {
            ArrayList listaActivos = new ArrayList();
            DateTime dt = DateTime.MinValue; // Começa sempre em -oo
            KeysManager km = new KeysManager();

            int port = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("[TestaTracker]Creating socket.");
            UDPSecureSocket uss = new UDPSecureSocket(port,km);
            Console.WriteLine("[TestaTracker] Socket created.");
            km.SendSocket = uss;
            km.ReceiveSocket = uss;

            while (true)
            {
                TrackerRequestMessage tr = new TrackerRequestMessage("127.0.0.1", port, dt, "A");
                Console.WriteLine("[TestaTracker] Sending");
                uss.sendMessage(tr, "127.0.0.1", 1245);
                Console.WriteLine("Sent!");
                TrackerAnswerMessage ta = (TrackerAnswerMessage)uss.receiveMessage();

                if (ta.ResponseCode == 0)
                {
                    // Sem update na lista de activos
                    Console.WriteLine("No new active list.");
                }
                else if (ta.ResponseCode == 1)
                {
                    // Com update na lista de activos, actualizar lista e timestamp
                    dt = ta.NewUpdateTime;
                    listaActivos = ta.ActiveNodeList;
                    Console.WriteLine(ta.ActiveNodeList.Count);
                    Console.WriteLine(dt);
                }
                Console.ReadLine();
            }
        }
    }
}
