using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetIRC.Messages.Receive
{
    class PrivateMessage : ReceivePrivMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            return ReceiveUserMessage.CheckCommand(message, "PRIVMSG") &&
                (ReceivePrivMessage.GetChannel(client, message) == null) &&
                !ReceivePrivMessage.IsCTCP(message);
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            User target = client.UserFactory.FromNick(parts[2].Substring(1));

            if (target == client.User)
            {
                User user = ReceiveUserMessage.GetUser(client, message);

                string line = String.Join(" ", parts.Skip(3).ToArray()).Substring(1);

                client.TriggerOnMessage(user, line);
            }
        }
    }
}
