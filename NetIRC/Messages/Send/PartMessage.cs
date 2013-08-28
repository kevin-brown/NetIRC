using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class PartMessage : SendMessage
    {
        private string channelName;
        private string message;

        public PartMessage(string channelName)
        {
            this.channelName = channelName;
        }

        public PartMessage(Channel channel)
        {
            this.channelName = channel.FullName;
        }

        public PartMessage(string channelName, string message)
        {
            this.channelName = channelName;
            this.message = message;
        }

        public PartMessage(Channel channel, string message)
        {
            this.channelName = channel.FullName;
            this.message = message;
        }

        public void Send(Client client, StreamWriter writer)
        {
            if (this.message == null)
            {
                writer.WriteLine("PART {0}", this.channelName);
            }
            else
            {
                writer.WriteLine("PART {0} :{1}", this.channelName, this.message);
            }
        }
    }
}
