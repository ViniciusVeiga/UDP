using System;
using System.Net.Sockets;
using System.Threading;

namespace UDP
{
    public class Program
    {
        public static void Main()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var thread = new Thread(Service<WriterAndGetter>.ReceiveMessage);
            var ip = new WriterAndGetter().Get("Digite o IP: ");

            thread.Start();
            while (true) {
                Client<WriterAndGetter>.Send(socket, ip);
            }
        }
    }
}

