using System;
using System.Linq;

namespace NetIRC.Messages.Send
{
    public class NoticeMessage : SendMessage
    {
        private Channel channel;
        private string message;

        public NoticeMessage(string channel, string message)
        {
            this.channel = ChannelFactory.FromName(channel);
            this.message = message;
        }

        public NoticeMessage(Channel channel, string message)
        {
            this.channel = channel;
            this.message = message;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("NOTICE #{0} {1}", this.channel.Name, this.message);
        }
    }
}
