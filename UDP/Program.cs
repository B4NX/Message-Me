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
        static UdpClient sender;
        static void Main(string[] args)
        {
            sender = new UdpClient(666);
            IPAddress address = getAddress();
            Console.WriteLine(address);
            sender.Connect(new IPEndPoint(address, 777));

            Console.Write("Message to send: ");
            String s = Console.ReadLine();
            byte[] wat = Encoding.UTF8.GetBytes(s);
            sender.Send(wat, wat.Length);

            Console.WriteLine("Done Sending");
            
            IPEndPoint remote = null;
            byte[] data = sender.Receive(ref remote);
            
            Console.WriteLine(ParseData(data));

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        /// <summary>
        /// Asks for the IP from the User
        /// </summary>
        /// <returns>The IPAddress provided by the user</returns>
        static IPAddress getAddress()
        {
            Console.Write("Enter the Address to connnect to: ");
            string ip = Console.ReadLine();

            IPAddress address;
            if (ip.ToUpper() == "LOCALHOST")
            {
                return IPAddress.Loopback;
            }
            else if (String.Empty == ip)
            {
                return IPAddress.Loopback;
            }
            else if (IPAddress.TryParse(ip, out address))
            {
                return address;
            }
            else
            {
                try
                {
                    return Dns.GetHostEntry(ip).AddressList[0];
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
            return IPAddress.Loopback;
        }
        /// <summary>
        /// Gets an array of bytes representing the string
        /// </summary>
        /// <param name="s">String to get bytes from</param>
        /// <returns>Array of bytes representing the string</returns>
        static byte[] getByte(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
        /// <summary>
        /// Returns a string representation of the byte array given
        /// </summary>
        /// <param name="b">Byte array to get the string from</param>
        /// <returns>String represented by the given array</returns>
        static string ParseData(byte[] b)
        {
            return Encoding.UTF8.GetString(b);
        }
    }
}
