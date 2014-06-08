using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Receiver
{
    class Receiver
    {
        static void Main(string[] args)
        {
            UdpClient client = new UdpClient();
            client.Client.Bind(new IPEndPoint(IPAddress.Any, 777));
            IPEndPoint endPoint = null;
            byte[] mssg = null;
            while (mssg == null)
            {
                mssg = client.Receive(ref endPoint);
            }
            PrintMessage(mssg);
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        static void PrintMessage(byte[] mssg)
        {
            foreach (byte b in mssg)
            {
                Console.WriteLine(b);
            }
        }

        static void ReceiveStuff(Socket s)
        {
            byte[] data = new byte[5];

            s.BeginReceive(data, 0, 5, SocketFlags.None, null, new Object());
        }
    }
}
