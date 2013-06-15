using System;

namespace NetIRC.Messages.Send
{
    public class PartMessage : SendMessage
    {
        private string channel;
        private string message;

        public PartMessage(string channel)
        {
            this.channel = channel;
        }

        public PartMessage(Channel channel)
        {
            this.channel = "#" + channel.Name;
        }

        public PartMessage(string channel, string message)
        {
            this.channel = channel;
            this.message = message;
        }

        public PartMessage(Channel channel, string message)
        {
            this.channel = "#" + channel.Name;
            this.message = message;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            if (this.message == null)
            {
                writer.WriteLine("PART " + this.channel);
            }
            else
            {
                writer.WriteLine("PART " + this.channel + " " + this.message);
            }
        }
    }
}
