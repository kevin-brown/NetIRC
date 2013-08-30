using System;
using System.IO;
using System.Linq;

namespace NetIRC.Messages.Send
{
    public class ChannelNotice : ISendMessage
    {
        private string channelName;
        private string message;

        public ChannelNotice(string channelName, string message)
        {
            this.channelName = channelName;
            this.message = message;
        }

        public ChannelNotice(Channel channel, string message)
        {
            this.channelName = channel.FullName;
            this.message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("NOTICE {0} :{1}",this.channelName, this.message);

            client.ChannelFactory.FromName(this.channelName).TriggerOnSendNotice(this.message);
        }
    }
}
