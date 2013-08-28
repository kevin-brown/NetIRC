using System;
using System.IO;
using System.Linq;

namespace NetIRC.Messages.Send
{
    public class NoticeMessage : SendMessage
    {
        private string channelName;
        private string message;

        public NoticeMessage(string channelName, string message)
        {
            this.channelName = channelName;
            this.message = message;
        }

        public NoticeMessage(Channel channel, string message)
        {
            this.channelName = channel.FullName;
            this.message = message;
        }

        public void Send(Client client, StreamWriter writer)
        {
            writer.WriteLine("NOTICE {0} :{1}",this.channelName, this.message);

            client.ChannelFactory.FromName(this.channelName).TriggerOnSendNotice(this.message);
        }
    }
}
