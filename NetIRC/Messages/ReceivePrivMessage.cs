using System;

namespace NetIRC.Messages
{
    abstract class ReceivePrivMessage : ReceiveUserMessage
    {
        public static Channel GetChannel(Client client, string message)
        {
            string[] parts = message.Split(' ');

            if (!parts[2].StartsWith("#"))
            {
                return null;
            }

            return client.ChannelFactory.FromName(parts[2].Substring(1));
        }

        public static bool CheckCTCP(string message, string ctcp)
        {
            string[] parts = message.Split(' ');

            if (parts.Length == 4)
            {
                parts[3] = parts[3].Substring(0, parts[3].Length - 1);
            }

            return parts[3].Substring(2).ToLower() == ctcp.ToLower();
        }

        public static bool IsCTCP(string message)
        {
            string[] parts = message.Split(' ');

            return parts[3].StartsWith(":\x001");
        }
    }
}
