using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Receiver
{
    class Receiver
    {
        static Queue<Message> dataQueue;
        static bool exit = false;
        static void Main(string[] args)
        {
            dataQueue = new Queue<Message>();

            Thread receiveThread = new Thread(new ThreadStart(Receive));
            receiveThread.Start();

            Message m;
            string s;
            while (true)
            {
                if (dataQueue.Count == 0)
                {
                    continue;
                }
                m = dataQueue.Dequeue();

                s = ParseData(m.Data);
                if (s.ToUpper() == "EXIT")
                {
                    exit = true;
                    break;
                }
                Console.WriteLine(s);
            }
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        static void Receive()
        {
            UdpClient reader = new UdpClient();
            reader.Client.Bind(new IPEndPoint(IPAddress.Any, 777));
            IPEndPoint remote;
            byte[] mssg;

            while (!exit)
            {
                remote = null;
                mssg = null;
                while (mssg == null)
                {
                    mssg = reader.Receive(ref remote);
                }
                dataQueue.Enqueue(new Message(mssg, remote));
            }
            reader.Close();
        }
        static string ParseData(byte[] b)
        {
            return Encoding.UTF8.GetString(b);
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
