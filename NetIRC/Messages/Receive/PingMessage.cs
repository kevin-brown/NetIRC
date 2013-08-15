using System;

namespace NetIRC.Messages.Receive
{
    class PingMessage : ReceiveMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            if (message.StartsWith("PING"))
            {
                return true;
            }

            return false;
        }

        public void ProcessMessage(string message, Client client)
        {
            string[] args = message.Split(' ');

            client.Send(new Messages.Send.PongMessage(args[1]));
        }
    }
}
