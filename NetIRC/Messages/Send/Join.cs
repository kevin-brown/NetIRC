using System.IO;

namespace NetIRC.Messages.Send
{
    public class Join : ISendMessage
    {
        private readonly string _channelName;

        public Join(string channelName)
        {
            this._channelName = channelName;
        }

        public Join(Channel channel)
        {
            this._channelName = channel.FullName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("JOIN {0}", this._channelName);
        }
    }
}
