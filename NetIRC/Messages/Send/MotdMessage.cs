using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class MotdMessage : SendMessage
    {
        public void Send(Client client, StreamWriter writer)
        {
            writer.WriteLine("MOTD");
        }
    }
}