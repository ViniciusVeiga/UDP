using System;
using System.Collections.Generic;
using System.Text;

namespace UDP
{
    public interface IWriterAndGetter
    {

        string Send { get; set; }
        void Write(string send, string ip = "");
        string Get(string send, string ip = "");
    }
}
