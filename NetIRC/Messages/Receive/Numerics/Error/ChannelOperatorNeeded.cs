﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetIRC.Messages.Receive.Numerics.Error
{
    public class ChannelOperatorNeeded : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "482";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            Channel channel = message.GetChannel(message.Parameters[1]);

            channel.TriggerOnChannelOperatorNeeded(channel, message.Parameters[2]);
        }
    }
}
