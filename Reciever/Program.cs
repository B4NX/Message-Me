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
            client.Client.Bind(new IPEndPoint(IPAddress.Any, 666));
            IPEndPoint endPoint = null;
            byte[] mssg = null;
            while (mssg==null)
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
    }
}
