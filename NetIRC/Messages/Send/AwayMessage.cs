using System;

namespace NetIRC.Messages.Send
{
    public class AwayMessage : SendMessage
    {
        public string reason;

        public AwayMessage(string reason)
        {
            this.reason = reason;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("AWAY :" + this.reason);
        }
    }
}
