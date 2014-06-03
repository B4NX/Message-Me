using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Sender
{
    class SendMain
    {
        static void Main(string[] args)
        {
            UdpClient sender = new UdpClient(777);
            Console.Write("Enter the IP address to connect: ");
            IPAddress address = IPAddress.Parse(Console.ReadLine());
            Console.WriteLine(address);
            sender.Connect(new IPEndPoint(address, 666));

            byte[] wat = { 1, 2, 3, 4, 5, 6, 7, 8, 89, 9 };
            sender.Send(wat, wat.Length);
            Console.WriteLine("Done");
            Console.ReadKey();
        }
        static byte[] getByte(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
    }
}
