using System.IO;

namespace NetIRC.Messages.Send
{
    public class UserPrivate : ISendMessage
    {
        public string Nick { get; set; }
        public string Message { get; set; }

        public UserPrivate(string nick, string message)
        {
            this.Nick = nick;
            this.Message = message;
        }

        public UserPrivate(User user, string message)
        {
            this.Nick = user.NickName;
            this.Message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("PRIVMSG {0} :{1}", this.Nick, this.Message);
        }
    }
}
