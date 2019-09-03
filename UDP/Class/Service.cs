using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP
{
    public class Service<T> 
        where T : IWriterAndGetter
    {
        private const int port = 60000;

        public static void ReceiveMessage()
        {
            var sender = Activator.CreateInstance<T>();
            var listener = new UdpClient(port);
            var groupEP = new IPEndPoint(IPAddress.Any, port);

            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    var message = Encoding.UTF8.GetString(bytes, 0, bytes.Length).ToString();
                    
                    sender.Write(message, groupEP.ToString().Split(':')[0]);
                }             
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }
    }
}

