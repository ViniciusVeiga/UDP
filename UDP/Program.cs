using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace UDP
{
    public class Program
    {
        public static void Main()
        {
            var ips = new List<string> {
                "172.18.0.99",
                "172.18.0.98",
                "172.18.0.107",
                "172.18.0.108",
                "172.18.0.90",
                "172.18.0.91",
                "172.18.0.238",
                "172.18.0.106",
                "172.18.0.84",
                "172.18.3.83",
                "172.18.3.87",
                "172.18.3.64",
                "172.18.3.61",
                "172.18.3.60",
                "172.18.3.74",
                "172.18.3.92",
                "172.18.3.105",
                "172.18.3.123",
                "172.18.3.137"
            };

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var thread = new Thread(Service<WriterAndGetter_2>.ReceiveMessage);

            thread.Start();
            while (true) {
                foreach (var ip in ips)
                {
                    Client<WriterAndGetter_2>.Send(socket, ip);
                }
            }
        }
    }
}

