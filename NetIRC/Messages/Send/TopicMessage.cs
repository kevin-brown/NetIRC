using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Send
{
    public class TopicMessage : SendMessage
    {
        string channel;
        string topic;

        public TopicMessage(string channel)
        {
            this.channel = channel;
        }

        public TopicMessage(Channel channel)
        {
            this.channel = "#" + channel.Name;
        }

        public TopicMessage(string channel, string topic)
        {
            this.channel = channel;
            this.topic = topic;
        }

        public TopicMessage(Channel channel, string topic)
        {
            this.channel = "#" + channel.Name;
            this.topic = topic;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            if (string.IsNullOrEmpty(this.topic))
            {
                writer.WriteLine("TOPIC " + this.channel);
            }
            else
            {
                writer.WriteLine(string.Format("TOPIC {0} {1}", this.channel, this.topic));
            }
        }
    }
}
