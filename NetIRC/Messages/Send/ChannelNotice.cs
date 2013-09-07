using System.IO;

namespace NetIRC.Messages.Send
{
    public class ChannelNotice : ISendMessage
    {
        public string ChannelName { get; set; }
        public string Message { get; set; }

        public ChannelNotice(string channelName, string message)
        {
            this.ChannelName = channelName;
            this.Message = message;
        }

        public ChannelNotice(Channel channel, string message)
        {
            this.ChannelName = channel.FullName;
            this.Message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("NOTICE {0} :{1}",this.ChannelName, this.Message);

            client.ChannelFactory.FromName(this.ChannelName).TriggerOnSendNotice(this.Message);
        }
    }
}
