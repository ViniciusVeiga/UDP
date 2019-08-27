using System;
using System.Collections.Generic;
using System.Text;

namespace UDP
{
    public interface IWriterAndGetter
    {
        void Write(string send, string ip = "");
        string Get(string send = "(Você): ");
    }
}
