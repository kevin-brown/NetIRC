using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class QuitMessage : ISendMessage
    {
        private string message;

        public QuitMessage()
        {
        }

        public QuitMessage(string message)
        {
            this.message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (this.message == null)
            {
                writer.WriteLine("QUIT");
            }
            else
            {
                writer.WriteLine("QUIT {0}", this.message);
            }
        }
    }
}
