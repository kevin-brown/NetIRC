using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class JoinMessage : SendMessage
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

        public void Send(Client client, StreamWriter writer)
        {
            writer.WriteLine("JOIN {0}", this.channelName);
        }
    }
}
