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
                "172.18.3.40",
                "172.18.0.99",
                "172.18.0.106",
                "172.18.2.176",
                "172.18.0.107",
                "172.18.3.137",
                "172.18.0.108",
                "172.18.0.86",
                "172.18.0.81",
                "172.18.3.66",
                "172.18.3.60",
                "172.18.0.83",
                "172.18.3.62",
                "172.18.0.33",
                "172.18.0.69",
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

