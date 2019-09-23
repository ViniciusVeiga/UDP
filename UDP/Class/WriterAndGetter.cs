using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UDP
{
    #region Projeto 01

    public class WriterAndGetter : IWriterAndGetter
    {
        public string Send { get => "(Você): "; }

        public void Write(string message, string ip)
        {
            Console.WriteLine(message);
        }

        public string Get(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public string Get(string send, string ip = "")
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region Projeto 02

    public class WriterAndGetter_2 : IWriterAndGetter
    {
        public const string request = "Heartbeat Request";
        public const string reply = "Heartbeat Reply";

        public string Send { get => "Heartbeat Request"; }

        public void Write(string message, string ip)
        {
            if (message.ToLower().Equals(request.ToLower()))
            {
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                Client<WriterAndGetter_2>.Send(socket, ip);

                Console.WriteLine($"\t\t\t\t{request} - {ip}");
                Console.WriteLine($"{reply} - {ip}");
            }

            if (message.ToLower().Equals(reply.ToLower()))
            {
                Console.WriteLine($"\t\t\t\t{reply} - {ip}");
            }
        }

        public string Get(string message, string ip = "")
        {
            Console.WriteLine($"{message} - {ip}");
            return message;
        }
    }

    #endregion

    #region Projeto 03

    public class WriterAndGetter_3 : IWriterAndGetter
    {
        private List<Tuple<int, string>> Candidates = new List<Tuple<int, string>>();
        private const string request = "Heartbeat Request";
        private const string reply = "Heartbeat Reply";

        public string Send { get => "Heartbeat Request"; }

        public void Write(string message, string ip)
        {
            var leader = GetLeader(ip);

            if (leader != null)
                Console.WriteLine($"Lider: {leader.Item2}");

            if (message.ToLower().Equals(request.ToLower()))
            {
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                Client<WriterAndGetter_2>.Send(socket, ip);
                AddCandidate(ip);
            }
        }

        public string Get(string message, string ip = "")
        {
            Console.WriteLine($"{message} - {ip}");
            return message;
        }

        public Tuple<int, string> GetLeader(string ip)
        {
            try
            {
                if (Candidates.Any(i => i.Item2 == ip))
                {
                    var min = Candidates.Select(o => o.Item1).Min();
                    return Candidates.Find(i => i.Item1.Equals(min));
                }
            }
            catch { }

            return null;
        }

        public void AddCandidate(string ip)
        {
            Candidates.Add(Program.Ips.Find(i => i.Item2 == ip));
        }
    }

    #endregion
}
