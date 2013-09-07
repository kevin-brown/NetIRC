using System.IO;

namespace NetIRC.Messages.Send
{
    public class ChannelMode : ISendMessage
    {
        public string ChannelName { get; set; }
        public string Modes { get; set; }
        public string Parameters { get; set; }

        public ChannelMode(string channelName)
        {
            this.ChannelName = channelName;
        }

        public ChannelMode(string channelName, string modes)
        {
            this.ChannelName = channelName;
            this.Modes = modes;
        }

        public ChannelMode(string channelName, string modes, string parameters)
        {
            this.ChannelName = channelName;
            this.Modes = modes;
            this.Parameters = parameters;
        }

        public ChannelMode(Channel channel)
        {
            this.ChannelName = channel.FullName;
        }

        public ChannelMode(Channel channel, string modes)
        {
            this.ChannelName = channel.FullName;
            this.Modes = modes;
        }

        public ChannelMode(Channel channel, string modes, string parameters)
        {
            this.ChannelName = channel.FullName;
            this.Modes = modes;
            this.Parameters = parameters;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (string.IsNullOrEmpty(this.Modes))
            {
                writer.WriteLine("MODE {0}", this.ChannelName);
            }

            else if (string.IsNullOrEmpty(this.Parameters))
            {
                writer.WriteLine("MODE {0} {1}", this.ChannelName, this.Modes);
            }

            else
            {
                writer.WriteLine("MODE {0} {1} {2}", this.ChannelName, this.Modes, this.Parameters);
            }
        }
    }
}
