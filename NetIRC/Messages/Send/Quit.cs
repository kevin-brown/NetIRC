using System.IO;

namespace NetIRC.Messages.Send
{
    public class Quit : ISendMessage
    {
        public string Message { get; set; }

        public Quit()
        {
        }

        public Quit(string message)
        {
            this.Message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (this.Message == null)
            {
                writer.WriteLine("QUIT");
            }
            else
            {
                writer.WriteLine("QUIT {0}", this.Message);
            }
        }
    }
}
