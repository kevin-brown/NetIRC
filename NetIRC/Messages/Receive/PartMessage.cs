using System;

namespace NetIRC.Messages.Receive
{
    class PartMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "PART";
        }

        public override void ProcessMessage(ParsedMessage message, Client client)
        {
            Channel channel = message.GetChannel();
            User user = message.GetUser();

            channel.RemoveUser(user);

            if (user == client.User)
            {
                client.TriggerOnChannelLeave(channel);
            }

            client.Send(channel.SendWho());
        }
    }
}
