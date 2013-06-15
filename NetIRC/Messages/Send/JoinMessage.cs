using System;

namespace NetIRC.Messages.Send
{
    public class JoinMessage : SendMessage
    {
        private Channel channel;

        public JoinMessage(string channel)
        {
            this.channel = ChannelFactory.FromName(channel);
        }

        public JoinMessage(Channel channel)
        {
            this.channel = channel;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("JOIN {0}{1}", Channel.TypeChars[this.channel.Type], this.channel.Name);
        }
    }
}
