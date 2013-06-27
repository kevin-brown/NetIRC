using System;

namespace NetIRC.Messages.Send
{
    public class WhoMessage : SendMessage
    {
        string target;

        public WhoMessage(string target)
        {
            this.target = target;
        }

        public WhoMessage(Channel channel)
        {
            this.target = Channel.TypeChars[channel.Type] + channel.Name;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("WHO " + this.target);
        }
    }
}
