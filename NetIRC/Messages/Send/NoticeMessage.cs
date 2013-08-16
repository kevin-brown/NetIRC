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

        public void Send(StreamWriter writer)
        {
            writer.WriteLine("NOTICE {0} :{1}",this.channelName, this.message);

            // TODO: Move OnSend message
            //this.channel.TriggerOnSendNotice(this.message);
        }
    }
}
