using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class Back : ISendMessage
    {
        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("BACK");
        }
    }
}
