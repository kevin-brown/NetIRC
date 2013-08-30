using System.IO;

namespace NetIRC.Messages.Send
{
    public class Quit : ISendMessage
    {
        private readonly string _message;

        public Quit()
        {
        }

        public Quit(string message)
        {
            this._message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (this._message == null)
            {
                writer.WriteLine("QUIT");
            }
            else
            {
                writer.WriteLine("QUIT {0}", this._message);
            }
        }
    }
}
