using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class NotAwayMessage : SendMessage
    {
        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("AWAY");
        }
    }
}
