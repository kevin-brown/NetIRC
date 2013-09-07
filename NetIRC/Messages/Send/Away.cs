using System.IO;

namespace NetIRC.Messages.Send
{
    public class Away : ISendMessage
    {
        public string Reason { get; set; }

        public Away(string reason)
        {
            this.Reason = reason;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("AWAY :{0}", this.Reason);
        }
    }
}
