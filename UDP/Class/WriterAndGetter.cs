using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace UDP
{
    public class WriterAndGetter : IWriterAndGetter
    {
        public void Write(string message, string ip = "")
        {
            Console.WriteLine(message);
        }

        public string Get(string message = "(Você): ")
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }

    public class WriterAndGetter_2 : IWriterAndGetter
    {
        public void Write(string message, string ip = "")
        {
            if (message.Equals("Heartbeat Request"))
            {
                var respost = "Heartbeat Reply";
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                Client<WriterAndGetter_2>.Send(socket, ip, respost);
                Console.WriteLine(respost);
            }
        }

        public string Get(string message = "Heartbeat Request")
        {
            Console.Write(message);
            return message;
        }
    }
}
