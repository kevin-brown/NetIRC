using System.IO;

namespace NetIRC.Messages.Send
{
    public class Join : ISendMessage
    {
        public string ChannelName { get; set; }

        public Join(string channelName)
        {
            this.ChannelName = channelName;
        }

        public Join(Channel channel)
        {
            this.ChannelName = channel.FullName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("JOIN {0}", this.ChannelName);
        }
    }
}
