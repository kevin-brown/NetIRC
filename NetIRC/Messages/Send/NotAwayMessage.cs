using System;

namespace NetIRC.Messages.Send
{
    public class NotAwayMessage : SendMessage
    {
        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("AWAY");
        }
    }
}
