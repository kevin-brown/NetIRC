using System.IO;

namespace NetIRC.Messages.Send
{
    public class ChannelPrivate : ISendMessage
    {
        public string ChannelName { get; set; }
        public string Message { get; set; }

        public ChannelPrivate(string channelName, string message)
        {
            this.ChannelName = channelName;
            this.Message = message;
        }

        public ChannelPrivate(Channel channel, string message)
        {
            this.ChannelName = channel.FullName;
            this.Message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("PRIVMSG {0} :{1}", this.ChannelName, this.Message);

            client.ChannelFactory.FromName(this.ChannelName).TriggerOnSendMessage(this.Message);
        }
    }
}
