using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static volatile UdpClient client;

        //static Queue<Message> sendQueue, receiveQueue;
        static bool exit = false;
        static void Main(string[] args)
        {
            client = new UdpClient();
            client.Client.Bind(new IPEndPoint(IPAddress.Any, 666));
            //receiveQueue = new Queue<Message>();
            //sendQueue = new Queue<Message>();
            
            Thread sendThread = new Thread(new ThreadStart(Send));
            sendThread.Start();
            Receive();

            sendThread.Abort();

            //Thread receiveThread = new Thread(new ThreadStart(Receive));
            //receiveThread.Start();
            //Send();
            Console.ReadLine();
        }

        static void Receive()
        {
            IPEndPoint remote;
            byte[] mssg;

            string s;
            while (!exit)
            {
                remote = null;
                mssg = null;
                while (mssg == null)
                {
                    mssg = client.Receive(ref remote);
                }

                s = Encoding.UTF8.GetString(mssg);
                Console.WriteLine(s);
                if (s.ToUpper() == "EXIT")
                {
                    Console.WriteLine("Exiting");
                    exit = true;
                    break;
                }
                Console.CursorLeft = 0;
                Console.WriteLine("Received: {0}",s);
                Console.Write("\nMessage: ");
            }
        }

        static void Send()
        {
            IPAddress address = getAddress();
            Console.WriteLine("Address to connect to is: {0}", address);
            client.Connect(new IPEndPoint(address, 666));

            Console.Write("\nMessage: ");

            string s;
            while (!exit)
            {
                s = Console.ReadLine();
                byte[] data = Encoding.UTF8.GetBytes(s);
                Console.WriteLine("Sending: {0}", s);
                client.Send(data, data.Length);
            }
            client.Close();
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
    }

    struct Message
    {
        readonly byte[] data;
        readonly IPEndPoint endPoint;
        public Message(byte[] _data, IPEndPoint _endPoint)
        {
            this.data = _data;
            this.endPoint = _endPoint;
        }
        public byte[] Data
        {
            get
            {
                return this.data;
            }
        }
        public IPEndPoint EndPoint
        {
            get
            {
                return this.endPoint;
            }
        }
    }
}
