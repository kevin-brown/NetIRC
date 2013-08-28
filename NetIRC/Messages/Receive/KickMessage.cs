using System;
using System.Linq;

namespace NetIRC.Messages.Receive
{
    class KickMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            return ReceiveUserMessage.CheckCommand(message, "KICK");
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            Channel channel = client.ChannelFactory.FromName(parts[2].Substring(1));

            User kicker = ReceiveUserMessage.GetUser(client, message);
            User user = client.UserFactory.FromNick(parts[3]);

            string reason = String.Join(" ", parts.Skip(4).ToArray()).Substring(1);

            channel.TriggerOnKick(kicker, user, reason);
        }
    }
}
