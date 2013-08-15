using System;

namespace NetIRC.Messages.Send
{
    public class PartMessage : SendMessage
    {
        private Channel channel;
        private string message;

        public PartMessage(string channel)
        {
            this.channel = ChannelFactory.FromName(channel);
        }

        public PartMessage(Channel channel)
        {
            this.channel = channel;
        }

        public PartMessage(string channel, string message)
        {
            this.channel = ChannelFactory.FromName(channel);
            this.message = message;
        }

        public PartMessage(Channel channel, string message)
        {
            this.channel = channel;
            this.message = message;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            if (this.message == null)
            {
                writer.WriteLine("PART {0}{1}", Channel.TypeChars[this.channel.Type], this.channel.Name);
            }
            else
            {
                writer.WriteLine("PART {0}{1} :{2}", Channel.TypeChars[this.channel.Type], this.channel.Name, this.message);
            }
        }
    }
}
