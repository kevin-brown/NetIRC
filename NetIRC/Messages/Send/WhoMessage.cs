using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class WhoMessage : ISendMessage
    {
        string target;

        public WhoMessage(string target)
        {
            this.target = target;
        }

        public WhoMessage(Channel channel)
        {
            this.target = channel.FullName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("WHO " + this.target);
        }
    }
}
