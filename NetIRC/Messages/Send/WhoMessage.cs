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

        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("WHO " + this.target);
        }
    }
}
