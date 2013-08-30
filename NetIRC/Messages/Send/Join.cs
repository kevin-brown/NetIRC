using System.IO;

namespace NetIRC.Messages.Send
{
    public class Join : ISendMessage
    {
        private string channelName;

        public Join(string channelName)
        {
            this.channelName = channelName;
        }

        public Join(Channel channel)
        {
            this.channelName = channel.FullName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("JOIN {0}", this.channelName);
        }
    }
}
