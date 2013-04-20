using System;

namespace NetIRC.Messages
{
    abstract class ReceiveNumericMessage : ReceiveMessage
    {
        public static bool CheckNumeric(string message, Server server, int numeric)
        {
            if (message.ToLower().StartsWith(":" + server.HostName))
            {
                string[] parts = message.Split(' ');
                int messageNumeric = 0;

                bool didParse = int.TryParse(parts[1], out messageNumeric);

                if (!didParse)
                {
                    return false;
                }

                if (messageNumeric == numeric)
                {
                    return true;
                }
            }

            return false;
        }

        abstract public void ProcessMessage(string message, Client client);
    }
}
