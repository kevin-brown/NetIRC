using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Send
{
    public class TopicMessage : SendMessage
    {
        Channel channel;
        string topic;

        public TopicMessage(string channel)
        {
            this.channel = ChannelFactory.FromName(channel);
        }

        public TopicMessage(Channel channel)
        {
            this.channel = channel;
        }

        public TopicMessage(string channel, string topic)
        {
            this.channel = ChannelFactory.FromName(channel);
            this.topic = topic;
        }

        public TopicMessage(Channel channel, string topic)
        {
            this.channel = channel;
            this.topic = topic;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            if (string.IsNullOrEmpty(this.topic))
            {
                writer.WriteLine("TOPIC {0}{1}", Channel.TypeChars[this.channel.Type], this.channel.Name);
            }
            else
            {
                writer.WriteLine(string.Format("TOPIC {0}{1} {2}", Channel.TypeChars[this.channel.Type], this.channel.Name, this.topic));
            }
        }
    }
}
