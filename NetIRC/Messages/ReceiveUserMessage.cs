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

        public User GetUser(string message)
        {
            string[] parts = message.Split(' ');
            string userMask = parts[0].Substring(1);

            return UserFactory.FromUserMask(userMask);
        }

        abstract public void ProcessMessage(string message, Client client);
    }
}
