using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class Away : ISendMessage
    {
        public string reason;

        public Away(string reason)
        {
            this.reason = reason;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("AWAY :{0}", this.reason);
        }
    }
}
