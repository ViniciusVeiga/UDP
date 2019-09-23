using System;
using System.Collections.Generic;
using System.Text;

namespace UDP
{
    public interface IWriterAndGetter
    {
        string Send { get; }
        void Write(string send, string ip);
        string Get(string send, string ip = "");
    }
}
