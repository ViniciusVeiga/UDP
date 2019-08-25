using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP
{
    public class Client<T>
        where T : IWriterAndGetter
    {
        private const int port = 11000;

        public static void Send(Socket socket, string ip)
        {
            var message = Activator.CreateInstance<T>().Get();
            var broadcast = IPAddress.Parse(ip);
            var sendbuf = Encoding.UTF8.GetBytes(message);
            var ep = new IPEndPoint(broadcast, port);

            socket.SendTo(sendbuf, ep);
        }
    }
}

