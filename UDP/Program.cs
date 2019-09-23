using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace UDP
{
    public class Program
    {
        public static List<Tuple<int, string>> Ips = new List<Tuple<int, string>> {
            Tuple.Create(0, "172.18.3.40"),
            Tuple.Create(1, "172.18.0.99"),
            Tuple.Create(2, "172.18.0.106"),
            Tuple.Create(3, "172.18.2.176"),
            Tuple.Create(4, "172.18.0.107"),
            Tuple.Create(5, "172.18.3.137"),
            Tuple.Create(6, "172.18.0.108"),
            Tuple.Create(7, "172.18.0.86"),
            Tuple.Create(8, "172.18.0.81"),
            Tuple.Create(9, "172.18.3.66"),
            Tuple.Create(10, "172.18.3.60"),
            Tuple.Create(11, "172.18.0.83"),
            Tuple.Create(12, "172.18.3.62"),
            Tuple.Create(13, "172.18.0.33"),
            Tuple.Create(14, "172.18.0.69")
        };


        public static void Main()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var thread = new Thread(Service<WriterAndGetter_3>.ReceiveMessage);

            thread.Start();
            while (true) {
                foreach (var ip in Ips)
                {
                    Client<WriterAndGetter_3>.Send(socket, ip.Item2);
                }
            }
        }
    }
}

