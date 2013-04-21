using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Send
{
    class ChatMessage : SendMessage
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
            writer.WriteLine("PRIVMSG #{0} {1}", this.channel.Name, this.message);
        }
    }
}
