using System;

namespace NetIRC.Messages.Receive
{
    class JoinMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            return ReceiveUserMessage.CheckCommand(message, "JOIN");
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');
            Channel channel = client.ChannelFactory.FromName(parts[2].ToLower().Substring(2));

            User user = ReceiveUserMessage.GetUser(client, message);

            if (user == client.User)
            {
                client.TriggerOnChannelJoin(channel);
            }

            channel.JoinUser(user);

            client.Send(channel.SendWho());
        }
    }
}
