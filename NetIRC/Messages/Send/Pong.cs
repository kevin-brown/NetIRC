using System.IO;

namespace NetIRC.Messages.Send
{
    public class Pong : ISendMessage
    {
        private readonly string _extra;

        public Pong(string extra)
        {
            this._extra = extra;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("PONG :{0}", this._extra);
        }
    }
}
