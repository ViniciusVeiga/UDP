using System;
using System.Collections.Generic;
using System.Text;

namespace UDP
{
    public class WriterAndGetter : IWriterAndGetter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public string Get(string message = "(Você): ")
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}
