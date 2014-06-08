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
            UdpClient sender = new UdpClient(666);
            IPAddress address = getAddress();
            Console.WriteLine(address);
            sender.Connect(new IPEndPoint(address, 777));

            byte[] wat = { 1, 2, 3, 4, 5, 6, 7, 8, 89, 9 };
            //byte[] wat = Encoding.UTF8.GetBytes("i'm a little tea pot");
            sender.Send(wat, wat.Length);


            Console.WriteLine("Done");
            Console.ReadKey();
        }
        /// <summary>
        /// Asks for the IP from the User
        /// </summary>
        /// <returns>The IPAddress provided by the user</returns>
        static IPAddress getAddress()
        {
            Console.Write("Enter the IP address to connnect to: ");
            string ip = Console.ReadLine();

            if (ip.ToUpper() == "localhost")
                return IPAddress.Loopback;

            IPAddress address;
            if (!IPAddress.TryParse(ip, out address))
            {
                address = IPAddress.Loopback;
            }
            return address;
        }
        static byte[] getByte(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
    }
}
