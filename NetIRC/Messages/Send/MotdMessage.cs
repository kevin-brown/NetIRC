using System;

namespace NetIRC.Messages.Send
{
    class MotdMessage : SendMessage
    {
        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("MOTD");
        }
    }
}