using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class AwayMessage : SendMessage
    {
        public string reason;

        public AwayMessage(string reason)
        {
            this.reason = reason;
        }

        public void Send(StreamWriter writer)
        {
            writer.WriteLine("AWAY :" + this.reason);
        }
    }
}
