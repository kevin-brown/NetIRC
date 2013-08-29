using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class MotdMessage : SendMessage
    {
        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("MOTD");
        }
    }
}