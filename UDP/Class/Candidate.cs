using System;
using System.Collections.Generic;
using System.Text;

namespace UDP.Class
{
    public class Candidate
    {
        public Candidate() { }

        public Candidate(int priority, string ip)
        {
            Priority = priority;
            Ip = ip;
        }

        public int Priority { get; set; }
        public string Ip { get; set; }
        public int CountSend { get; set; }
        public int CountReceive { get; set; }
        public bool DeadOrNot { get; set; } = false;
    }
}
