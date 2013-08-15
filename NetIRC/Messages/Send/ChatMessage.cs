﻿using System;

namespace NetIRC.Messages.Send
{
    public class ChatMessage : SendMessage
    {
        Channel channel;
        string message;

        public ChatMessage(string channel, string message)
        {
            this.channel = ChannelFactory.FromName(channel);
            this.message = message;
        }

        public ChatMessage(Channel channel, string message)
        {
            this.channel = channel;
            this.message = message;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("PRIVMSG {0}{1} :{2}", Channel.TypeChars[this.channel.Type], this.channel.Name, this.message);
            this.channel.TriggerOnSendMessage(this.message);
        }
    }
}
