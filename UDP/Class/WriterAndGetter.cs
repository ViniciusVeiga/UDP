using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UDP.Class;

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

        public string Get(string send, Candidate candidate)
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

        public string Get(string send, Candidate candidate)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region Projeto 03

    public class WriterAndGetter_3 : IWriterAndGetter
    {
        private const string request = "Heartbeat Request";
        private const string reply = "Heartbeat Reply";
        public string Send { get => request; }
        private Candidate oldLeader = new Candidate();

        public void Write(string message, string ip)
        {
            if (message.ToLower().Equals(request.ToLower()))
            {
                var candidate = GetCandidate(ip); // Pega Candidato
                ResetIfIsDead(candidate); // Revive o Candidato

                var leader = GetLeader(ip); // Calcula o Lider
                WriteLeader(leader); // Printa o Lider

                // Responde para quem está vivo
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                Client<WriterAndGetter_4>.Send(socket, candidate);
            }
        }

        public string Get(string send, Candidate candidate)
        {
            if (!candidate.DeadOrNot)
                Console.WriteLine($"Vivo: {candidate.Ip} | Prioridade: {candidate.Priority}");
            else
                Console.WriteLine($"Morto: {candidate.Ip} | Prioridade: {candidate.Priority}");

            candidate.CountSend += 1; // Conta o Envio

            SetIfIsDead(candidate); // Olha se não está morto

            return send;
        }

        #region Methods Helpers

        public void ResetIfIsDead(Candidate candidate)
        {
            if (candidate.DeadOrNot == true)
            {
                candidate.DeadOrNot = false;
                candidate.CountReceive = 0;
                candidate.CountSend = 0;
            }
            else
                candidate.CountReceive += 1;

        }

        public void SetIfIsDead(Candidate candidate)
        {
            if (candidate.CountSend - candidate.CountReceive > 2 && candidate.DeadOrNot == false)
            {
                candidate.DeadOrNot = true;
            }
        }

        public void WriteLeader(Candidate leader)
        {
            if (leader != null)
            {
                if (oldLeader.Ip != leader.Ip)
                {
                    Console.WriteLine($"Lider: {leader.Ip}");
                    oldLeader = leader;
                }
            }
            else
            {
                Console.WriteLine($"Lider: None");
            }
        }

        public Candidate GetCandidate(string ip)
        {
            return Program.Ips.Find(i => i.Ip == ip);
        }

        public Candidate GetLeader(string ip)
        {
            try
            {
                var min = Program.Ips
                    .Where(i => i.DeadOrNot == false)
                    .Select(o => o.Priority)
                    .Min();

                return Program.Ips.Find(i => i.Priority.Equals(min));
            }
            catch { }

            return null;
        }

        public string Get(string send, string ip)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class WriterAndGetter_4 : IWriterAndGetter
    {
        private const string request = "Heartbeat Request";
        private const string reply = "Heartbeat Reply";
        public string Send { get => request; }

        public string Get(string send, string ip)
        {
            Console.WriteLine($"Vivo: {ip}");
            return send;
        }

        public string Get(string send, Candidate candidate)
        {
            //Console.WriteLine($"Vivo: {candidate.Ip} | Prioridade: {candidate.Priority}");

            return send;
        }

        public void Write(string send, string ip)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
