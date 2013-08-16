using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class NotAwayMessage : SendMessage
    {
        public void Send(StreamWriter writer)
        {
            writer.WriteLine("AWAY");
        }
    }
}
