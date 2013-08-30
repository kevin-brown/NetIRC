using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class JoinMessage : ISendMessage
    {
        private string channelName;

        public JoinMessage(string channelName)
        {
            this.channelName = channelName;
        }

        public JoinMessage(Channel channel)
        {
            this.channelName = channel.FullName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("JOIN {0}", this.channelName);
        }
    }
}
