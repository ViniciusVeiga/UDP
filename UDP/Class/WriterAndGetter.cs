using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace UDP
{
    public class WriterAndGetter : IWriterAndGetter
    {
        string IWriterAndGetter.Send { get => "(Você): "; set => throw new NotImplementedException(); }

        public void Write(string message, string ip = "")
        {
            Console.WriteLine(message);
        }

        public string Get(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        void IWriterAndGetter.Write(string send, string ip)
        {
            throw new NotImplementedException();
        }

        string IWriterAndGetter.Get(string send)
        {
            throw new NotImplementedException();
        }
    }

    public class WriterAndGetter_2 : IWriterAndGetter
    {
        public const string request = "Heartbeat Request";
        public const string reply = "Heartbeat Reply";

        public string Send { get => "Heartbeat Request"; set => throw new NotImplementedException(); }

        public void Write(string message, string ip = "")
        {
            if (message.Equals(request))
            {
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                Client<WriterAndGetter_2>.Send(socket, ip);
            }

            Console.WriteLine(reply);
        }

        public string Get(string message)
        {
            Console.WriteLine(message);
            return message;
        }
    }
}
