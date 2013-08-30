﻿using System;

namespace NetIRC.Messages.Receive
{
    class JoinMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "JOIN";
        }

        public override void ProcessMessage(ParsedMessage message, Client client)
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
