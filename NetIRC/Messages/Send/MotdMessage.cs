using System;

namespace NetIRC.Messages.Send
{
    public class MotdMessage : SendMessage
    {
        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("MOTD");
        }
    }
}