using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Send
{
    public class ChannelModeMessage : SendMessage
    {
        private string channelName;
        private string modes;
        private string parameters;

        public ChannelModeMessage(string channelName)
        {
            this.channelName = channelName;
        }

        public ChannelModeMessage(string channelName, string modes)
        {
            this.channelName = channelName;
            this.modes = modes;
        }

        public ChannelModeMessage(string channelName, string modes, string parameters)
        {
            this.channelName = channelName;
            this.modes = modes;
            this.parameters = parameters;
        }

        public ChannelModeMessage(Channel channel)
        {
            this.channelName = channel.FullName;
        }

        public ChannelModeMessage(Channel channel, string modes)
        {
            this.channelName = channel.FullName;
            this.modes = modes;
        }

        public ChannelModeMessage(Channel channel, string modes, string parameters)
        {
            this.channelName = channel.FullName;
            this.modes = modes;
            this.parameters = parameters;
        }

        public void Send(StreamWriter writer)
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
