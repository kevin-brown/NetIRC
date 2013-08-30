using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class Away : ISendMessage
    {
        private string reason;

        public Away()
        {
        }

        public Away(string reason)
        {
            this.reason = reason;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (string.IsNullOrEmpty(this.reason))
            {
                writer.WriteLine("AWAY");
            }

            else
            {
                writer.WriteLine("AWAY :{0}", this.reason);
            }
        }
    }
}
