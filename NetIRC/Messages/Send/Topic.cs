using System.IO;

namespace NetIRC.Messages.Send
{
    public class Topic : ISendMessage
    {
        private readonly string _channelName;
        private readonly string _topic;

        public Topic(string channelName)
        {
            this._channelName = channelName;
        }

        public Topic(Channel channel)
        {
            this._channelName = channel.FullName;
        }

        public Topic(string channelName, string topic)
        {
            this._channelName = channelName;
            this._topic = topic;
        }

        public Topic(Channel channel, string topic)
        {
            this._channelName = channel.FullName;
            this._topic = topic;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (string.IsNullOrEmpty(this._topic))
            {
                writer.WriteLine("TOPIC {0}", this._channelName);
            }
            else
            {
                writer.WriteLine("TOPIC {0} :{1}", this._channelName, this._topic);
            }
        }
    }
}
