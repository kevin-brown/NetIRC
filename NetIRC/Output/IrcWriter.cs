using System;

namespace NetIRC.Output
{
    class IrcWriter : Writer
    {
        public void ProcessSendMessage(string message, Client client)
        {
            lock(client.Writer)
                client.Writer.WriteLine(message);
        }

        public void ProcessReadMessage(string message, Client client)
        {
            
        }
    }
}
