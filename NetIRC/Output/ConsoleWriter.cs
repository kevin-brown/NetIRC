using System;

namespace NetIRC.Output
{
    class ConsoleWriter : Writer
    {
        public void ProcessMessage(string message, Client client)
        {
            Console.WriteLine(string.Format("[{0:HH:mm:ss}] > {1}", DateTime.Now, message));
        }
    }
}
