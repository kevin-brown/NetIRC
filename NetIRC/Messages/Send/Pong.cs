using System.IO;

namespace NetIRC.Messages.Send
{
    public class Pong : ISendMessage
    {
        private string extra;

        public Pong(string extra)
        {
            this.extra = extra;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("PONG :{0}", this.extra);
        }
    }
}
