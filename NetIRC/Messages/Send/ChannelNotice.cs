using System.IO;

namespace NetIRC.Messages.Send
{
    public class ChannelNotice : ISendMessage
    {
        private readonly string _channelName;
        private readonly string _message;

        public ChannelNotice(string channelName, string message)
        {
            this._channelName = channelName;
            this._message = message;
        }

        public ChannelNotice(Channel channel, string message)
        {
            this._channelName = channel.FullName;
            this._message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("NOTICE {0} :{1}",this._channelName, this._message);

            client.ChannelFactory.FromName(this._channelName).TriggerOnSendNotice(this._message);
        }
    }
}
