using System.IO;

namespace NetIRC.Messages.Send
{
    public class ChannelMode : ISendMessage
    {
        private string channelName;
        private string modes;
        private string parameters;

        public ChannelMode(string channelName)
        {
            this.channelName = channelName;
        }

        public ChannelMode(string channelName, string modes)
        {
            this.channelName = channelName;
            this.modes = modes;
        }

        public ChannelMode(string channelName, string modes, string parameters)
        {
            this.channelName = channelName;
            this.modes = modes;
            this.parameters = parameters;
        }

        public ChannelMode(Channel channel)
        {
            this.channelName = channel.FullName;
        }

        public ChannelMode(Channel channel, string modes)
        {
            this.channelName = channel.FullName;
            this.modes = modes;
        }

        public ChannelMode(Channel channel, string modes, string parameters)
        {
            this.channelName = channel.FullName;
            this.modes = modes;
            this.parameters = parameters;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (string.IsNullOrEmpty(this.modes))
            {
                writer.WriteLine("MODE {0}", this.channelName);
            }

            else if (string.IsNullOrEmpty(this.parameters))
            {
                writer.WriteLine("MODE {0} {1}", this.channelName, this.modes);
            }

            else
            {
                writer.WriteLine("MODE {0} {1} {2}", this.channelName, this.modes, this.parameters);
            }
        }
    }
}
