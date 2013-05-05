using System;

namespace NetIRC.Messages.Receive
{
    class PartMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            return ReceiveUserMessage.CheckCommand(message, "PART");
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');
            Channel channel = ChannelFactory.FromName(parts[2].ToLower().Substring(1));

            User user = this.GetUser(message);

            channel.RemoveUser(user);

            if (user == client.User)
            {
                client.TriggerOnChannelLeave(channel);
            }

            client.Send(channel.SendWho());
        }
    }
}
