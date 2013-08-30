using System.IO;

namespace NetIRC.Messages.Send
{
    public class Away : ISendMessage
    {
        private readonly string _reason;

        public Away(string reason)
        {
            this._reason = reason;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("AWAY :{0}", this._reason);
        }
    }
}
