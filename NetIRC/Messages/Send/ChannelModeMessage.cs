using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Send
{
    public class ChannelModeMessage : SendMessage
    {
        private Channel channel;
        private string modes;
        private string parameters;

        public ChannelModeMessage(Channel channel)
        {
            this.channel = channel;
        }

        public ChannelModeMessage(Channel channel, string modes)
        {
            this.channel = channel;
            this.modes = modes;
        }

        public ChannelModeMessage(Channel channel, string modes, string parameters)
        {
            this.channel = channel;
            this.modes = modes;
            this.parameters = parameters;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            if (string.IsNullOrEmpty(this.modes))
            {
                writer.WriteLine("MODE {0}{1}", Channel.TypeChars[this.channel.Type], this.channel.Name);
            }

            else if (string.IsNullOrEmpty(this.parameters))
            {
                writer.WriteLine("MODE {0}{1} {2}", Channel.TypeChars[this.channel.Type], this.channel.Name, this.modes);
            }

            else
            {
                writer.WriteLine("MODE {0}{1} {2} {3}", Channel.TypeChars[this.channel.Type], this.channel.Name, this.modes, this.parameters);
            }
        }
    }
}
