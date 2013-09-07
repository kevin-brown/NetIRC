using System.IO;

namespace NetIRC.Messages.Send
{
    public class Topic : ISendMessage
    {
        public string ChannelName { get; set; }
        public string NewTopic { get; set; }

        public Topic(string channelName)
        {
            this.ChannelName = channelName;
        }

        public Topic(Channel channel)
        {
            this.ChannelName = channel.FullName;
        }

        public Topic(string channelName, string newTopic)
        {
            this.ChannelName = channelName;
            this.NewTopic = newTopic;
        }

        public Topic(Channel channel, string newTopic)
        {
            this.ChannelName = channel.FullName;
            this.NewTopic = newTopic;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (string.IsNullOrEmpty(this.NewTopic))
            {
                writer.WriteLine("TOPIC {0}", this.ChannelName);
            }
            else
            {
                writer.WriteLine("TOPIC {0} :{1}", this.ChannelName, this.NewTopic);
            }
        }
    }
}
