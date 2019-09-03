using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UDP
{
    public class Client<T>
        where T : IWriterAndGetter
    {
        private const int port = 60000;

        public static void Send(Socket socket, string ip)
        {
            var getter = Activator.CreateInstance<T>();
            var message = getter.Get(getter.Send, ip);
            var broadcast = IPAddress.Parse(ip);
            var sendbuf = Encoding.UTF8.GetBytes(message);
            var ep = new IPEndPoint(broadcast, port);

            socket.SendTo(sendbuf, ep);
            Thread.Sleep(1000);
        }
    }
}

