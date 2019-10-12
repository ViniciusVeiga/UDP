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

                // Responde para quem está vivo
                if (candidate != null)
                {
                    var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    Client<WriterAndGetter_4>.Send(socket, candidate);
                }
                //Descomentar Depois
                Console.WriteLine($"\t\t\t\t{request} - {ip}");
                Console.WriteLine($"{reply} - {ip}");
            }

            //Descomentar Depois
            if (message.ToLower().Equals(reply.ToLower()))
            {
                var candidate = GetCandidate(ip); // Pega Candidato
                ResetIfIsDead(candidate); // Revive o Candidato

                var leader = GetLeader(ip); // Calcula o Lider
                WriteLeader(leader); // Printa o Lider

                Console.WriteLine($"\t\t\t\t{reply} - {ip}");
            }
        }

        public string Get(string send, Candidate candidate)
        {
            //if (!candidate.DeadOrNot)
            //    Console.WriteLine($"Vivo: {candidate.Ip} | Prioridade: {candidate.Priority}");
            //else
            //    Console.WriteLine($"Morto: {candidate.Ip} | Prioridade: {candidate.Priority}");

            Console.WriteLine($"{send} - {candidate.Ip}");// Descomentar Depois

            SetIfIsDead(candidate); // Olha se não está morto
            candidate.Count += 1; // Conta o Envio

            return send;
        }

        #region Methods Helpers

        public void ResetIfIsDead(Candidate candidate)
        {
            if (candidate != null)
            {
                candidate.DeadOrNot = false;
                candidate.Count = 0;
            }
        }

        public void SetIfIsDead(Candidate candidate)
        {
            if (candidate.Count > 1 && candidate.DeadOrNot == false)
            {
                candidate.DeadOrNot = true;
            }
        }

        public void WriteLeader(Candidate leader)
        {
            if (leader != null)
                Console.WriteLine($"\t\t\t\t\t\t\t\tLider: {leader.Ip} - Prioridade: {leader.Priority}");
        }

        public Candidate GetCandidate(string ip)
        {
            return Program.Ips.Find(i => i.Ip == ip);
        }

        public Candidate GetLeader(string ip)
        {
            try
            {
                var leader = Program.Ips
                    .Where(i => i.DeadOrNot == false)
                    .OrderBy(x => x.Priority)
                    .First();

                //if (oldLeader != leader)
                //{
                    oldLeader = leader;
                    return leader;
                //}
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
