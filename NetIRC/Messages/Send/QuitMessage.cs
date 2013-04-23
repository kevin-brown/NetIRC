using System;

namespace NetIRC.Messages.Send
{
    public class QuitMessage : SendMessage
    {
        private string message;

        public QuitMessage()
        {

        }

        public QuitMessage(string message)
        {
            this.message = message;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            if (this.message == null)
            {
                writer.WriteLine("QUIT");
            }
            else
            {
                writer.WriteLine("QUIT " + this.message);
            }
        }
    }
}
