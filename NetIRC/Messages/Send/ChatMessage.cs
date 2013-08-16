﻿using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class ChatMessage : SendMessage
    {
        string channelName;
        string message;

        public ChatMessage(string channelName, string message)
        {
            this.channelName = channelName;
            this.message = message;
        }

        public ChatMessage(Channel channel, string message)
        {
            this.channelName = channel.FullName;
            this.message = message;
        }

        public void Send(StreamWriter writer)
        {
            writer.WriteLine("PRIVMSG {0} :{1}", channelName, this.message);
            this.channel.TriggerOnSendMessage(this.message);
        }
    }
}
