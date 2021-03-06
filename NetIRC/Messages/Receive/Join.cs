﻿namespace NetIRC.Messages.Receive
{
    public class Join : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "JOIN";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User user = message.GetUser();
            Channel channel = message.GetChannel();

            channel.JoinUser(user);

            if (user == client.User)
            {
                client.TriggerOnChannelJoin(channel);
            }

            client.Send(channel.SendWho());
        }
    }
}
