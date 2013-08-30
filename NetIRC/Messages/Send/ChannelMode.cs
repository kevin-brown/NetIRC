using System.IO;

namespace NetIRC.Messages.Send
{
    public class ChannelMode : ISendMessage
    {
        private readonly string _channelName;
        private readonly string _modes;
        private readonly string _parameters;

        public ChannelMode(string channelName)
        {
            this._channelName = channelName;
        }

        public ChannelMode(string channelName, string modes)
        {
            this._channelName = channelName;
            this._modes = modes;
        }

        public ChannelMode(string channelName, string modes, string parameters)
        {
            this._channelName = channelName;
            this._modes = modes;
            this._parameters = parameters;
        }

        public ChannelMode(Channel channel)
        {
            this._channelName = channel.FullName;
        }

        public ChannelMode(Channel channel, string modes)
        {
            this._channelName = channel.FullName;
            this._modes = modes;
        }

        public ChannelMode(Channel channel, string modes, string parameters)
        {
            this._channelName = channel.FullName;
            this._modes = modes;
            this._parameters = parameters;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (string.IsNullOrEmpty(this._modes))
            {
                writer.WriteLine("MODE {0}", this._channelName);
            }

            else if (string.IsNullOrEmpty(this._parameters))
            {
                writer.WriteLine("MODE {0} {1}", this._channelName, this._modes);
            }

            else
            {
                writer.WriteLine("MODE {0} {1} {2}", this._channelName, this._modes, this._parameters);
            }
        }
    }
}
