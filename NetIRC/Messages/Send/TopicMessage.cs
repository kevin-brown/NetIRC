using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Send
{
    public class TopicMessage : SendMessage
    {
        string channelName;
        string topic;

        public TopicMessage(string channelName)
        {
            this.channelName = channelName;
        }

        public TopicMessage(Channel channel)
        {
            this.channelName = channel.FullName;
        }

        public TopicMessage(string channelName, string topic)
        {
            this.channelName = channelName;
            this.topic = topic;
        }

        public TopicMessage(Channel channel, string topic)
        {
            this.channelName = channel.FullName;
            this.topic = topic;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (string.IsNullOrEmpty(this.topic))
            {
                writer.WriteLine("TOPIC {0}", this.channelName);
            }
            else
            {
                writer.WriteLine("TOPIC {0} :{1}", this.channelName, this.topic);
            }
        }
    }
}
