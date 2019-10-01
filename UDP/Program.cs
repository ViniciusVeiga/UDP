using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UDP.Class;

namespace UDP
{
    public class Program
    {
        public static List<Candidate> Ips = new List<Candidate> {
            new Candidate(0, "192.168.15.25"),
            new Candidate(1, "192.168.15.25"),
            new Candidate(2, "192.168.15.25"),
            new Candidate(3, "192.168.15.25")
        };

        public static void Main()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var thread = new Thread(Service<WriterAndGetter_3>.ReceiveMessage);

            thread.Start();
            while (true)
            {
                foreach (var ip in Ips)
                {
                    Client<WriterAndGetter_3>.Send(socket, ip);
                }
            }
        }
    }
}

