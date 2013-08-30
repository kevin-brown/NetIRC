using System;

namespace NetIRC.Messages
{
    abstract class ReceiveUserMessage : ReceiveMessage
    {
        public static bool CheckCommand(string message, string command)
        {
            if (!message.StartsWith(":"))
            {
                return false;
            }

            string[] parts = message.Split(' ');

            if (parts.Length < 2)
            {
                return false;
            }

            if (parts[1].ToLower() != command.ToLower())
            {
                return false;
            }

            return true;
        }

        public static User GetUser(Client client, string message)
        {
            string[] parts = message.Split(' ');
            string userMask = parts[0].Substring(1);

            return client.UserFactory.FromUserMask(userMask);
        }

        abstract public void ProcessMessage(ParsedMessage message, Client client);
    }
}
