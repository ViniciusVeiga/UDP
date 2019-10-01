using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UDP.Class;

namespace UDP
{
    public class Client<T>
        where T : IWriterAndGetter
    {
        private const int port = 60000;

        public static void Send(Socket socket, string ip)
        {
            Send(socket, new Candidate(-1, ip));
        }

        public static void Send(Socket socket, Candidate candidate)
        {
            var getter = Activator.CreateInstance<T>();
            var message = getter.Get(getter.Send, candidate);
            var broadcast = IPAddress.Parse(candidate.Ip);
            var sendbuf = Encoding.UTF8.GetBytes(message);
            var ep = new IPEndPoint(broadcast, port);

            socket.SendTo(sendbuf, ep);
            Thread.Sleep(1000);
        }
    }
}

