using System;

namespace NetIRC.Output
{
    class ConsoleWriter : Writer
    {
        public void ProcessSendMessage(string message, Client client)
        {
            Console.WriteLine("[{0:HH:mm:ss}] > {1}", DateTime.Now, message);
        }


        public void ProcessReadMessage(string message, Client client)
        {
            Console.WriteLine("[{0:HH:mm:ss}] < {1}", DateTime.Now, message);
        }
    }
}
