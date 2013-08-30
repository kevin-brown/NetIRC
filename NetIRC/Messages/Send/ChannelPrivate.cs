using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class ChannelPrivate : ISendMessage
    {
        string channelName;
        string message;

        public ChannelPrivate(string channelName, string message)
        {
            this.channelName = channelName;
            this.message = message;
        }

        public ChannelPrivate(Channel channel, string message)
        {
            this.channelName = channel.FullName;
            this.message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("PRIVMSG {0} :{1}", this.channelName, this.message);

            client.ChannelFactory.FromName(this.channelName).TriggerOnSendMessage(this.message);
        }
    }
}
