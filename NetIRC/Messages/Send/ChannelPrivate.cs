using System.IO;

namespace NetIRC.Messages.Send
{
    public class ChannelPrivate : ISendMessage
    {
        private readonly string _channelName;
        private readonly string _message;

        public ChannelPrivate(string channelName, string message)
        {
            this._channelName = channelName;
            this._message = message;
        }

        public ChannelPrivate(Channel channel, string message)
        {
            this._channelName = channel.FullName;
            this._message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("PRIVMSG {0} :{1}", this._channelName, this._message);

            client.ChannelFactory.FromName(this._channelName).TriggerOnSendMessage(this._message);
        }
    }
}
