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
            new Candidate(1, "172.18.0.99"),
            new Candidate(2, "172.18.0.106"),
            new Candidate(3, "172.18.2.176"),
            new Candidate(4, "172.18.0.107"),
            new Candidate(5, "172.18.3.137"),
            new Candidate(6, "172.18.0.108"),
            new Candidate(7, "172.18.0.86"),
            new Candidate(8, "172.18.0.81"),
            new Candidate(9, "172.18.3.66"),
            new Candidate(10, "172.18.3.60"),
            new Candidate(11, "172.18.0.83"),
            new Candidate(12, "172.18.3.62"),
            new Candidate(13, "172.18.0.33"),
            new Candidate(14, "172.18.0.69")
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
                    Client<WriterAndGetter_3>.Send(socket, ip.Ip);
                }
            }
        }
    }
}

