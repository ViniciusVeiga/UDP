using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP
{
    public class Client<T>
        where T : IWriterAndGetter
    {
        private const int port = 61000;

        public static void Send(Socket socket, string ip, string message_2 = null)
        {
            var message = Activator.CreateInstance<T>().Get(message_2);
            var broadcast = IPAddress.Parse(ip);
            var sendbuf = Encoding.UTF8.GetBytes(message);
            var ep = new IPEndPoint(broadcast, port);

            socket.SendTo(sendbuf, ep);
        }
    }
}

