using System.IO;

namespace NetIRC.Messages.Send
{
    public class UserPrivate : ISendMessage
    {
        private string nick;
        private string message;

        public UserPrivate(string nick, string message)
        {
            this.nick = nick;
            this.message = message;
        }

        public UserPrivate(User user, string message)
        {
            this.nick = user.NickName;
            this.message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("PRIVMSG {0} :{1}", this.nick, this.message);
        }
    }
}
