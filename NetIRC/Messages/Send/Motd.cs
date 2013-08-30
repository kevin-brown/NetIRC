using System.IO;

namespace NetIRC.Messages.Send
{
    public class Motd : ISendMessage
    {
        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("MOTD");
        }
    }
}