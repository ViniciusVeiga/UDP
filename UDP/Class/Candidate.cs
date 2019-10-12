using System;
using System.Collections.Generic;
using System.Text;

namespace UDP.Class
{
    public class Candidate
    {
        public Candidate() { }

        public Candidate(int priority, string ip, bool dead = true)
        {
            Priority = priority;
            Ip = ip;
            DeadOrNot = dead;
        }

        public int Priority { get; set; }
        public string Ip { get; set; }
        public int Count { get; set; }
        public bool DeadOrNot { get; set; } = false;
    }
}
