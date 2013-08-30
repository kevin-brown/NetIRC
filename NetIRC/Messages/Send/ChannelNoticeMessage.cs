using System;
using System.IO;
using System.Linq;

namespace NetIRC.Messages.Send
{
    public class ChannelNoticeMessage : ISendMessage
    {
        private string channelName;
        private string message;

        public ChannelNoticeMessage(string channelName, string message)
        {
            this.channelName = channelName;
            this.message = message;
        }

        public ChannelNoticeMessage(Channel channel, string message)
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
