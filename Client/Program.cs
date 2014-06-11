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
        static UdpClient client;

        //static Queue<Message> sendQueue, receiveQueue;
        static bool exit = false;
        static void Main(string[] args)
        {
            //receiveQueue = new Queue<Message>();
            //sendQueue = new Queue<Message>();

            Thread sendThread = new Thread(new ThreadStart(Send));
            sendThread.Start();
            Receive();

            //Thread receiveThread = new Thread(new ThreadStart(Receive));
            //receiveThread.Start();
            //Send();

        }

        static void Receive()
        {
            UdpClient reader = new UdpClient();
            reader.Client.Bind(new IPEndPoint(IPAddress.Any, 777));
            IPEndPoint remote;
            byte[] mssg;

            string s;
            while (!exit)
            {
                remote = null;
                mssg = null;
                while (mssg == null)
                {
                    mssg = reader.Receive(ref remote);
                }

                s = Encoding.UTF8.GetString(mssg);
                Console.WriteLine(s);
                if (s.ToUpper() == "EXIT")
                {
                    exit = true;
                    break;
                }
                Console.CursorLeft = 0;
                Console.WriteLine(s);
            }
            reader.Close();
        }

        static void Send()
        {
            UdpClient sender = new UdpClient(666);
            IPAddress address = getAddress();
            Console.WriteLine("Address to connect to is: {0}", address);
            sender.Connect(new IPEndPoint(address, 777));

            while (!exit)
            {
                byte[] data = Encoding.UTF8.GetBytes(Console.ReadLine());
                sender.Send(data, data.Length);
            }

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
