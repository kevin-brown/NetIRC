﻿using System;
using System.Linq;

namespace NetIRC.Messages.Receive
{
    class KickMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "KICK";
        }

        public override void ProcessMessage(ParsedMessage message, Client client)
        {
            User kicker = message.GetUser();
            Channel channel = message.GetChannel();
            User user = message.GetUserFromNick(message.Parameters[1]);
            string reason = message.Parameters[2];

            channel.TriggerOnKick(kicker, user, reason);
        }
    }
}
