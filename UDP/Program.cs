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
                "172.18.3.68",
                "172.18.3.72",
                "172.18.3.69",
                "172.18.3.129",
                //"172.18.3.124",
                "172.18.3.97",
                "172.18.3.108",
                "172.18.0.81",
                "172.18.3.137",
                "172.18.3.115",
                "172.18.0.83",
                "172.18.0.84"
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

