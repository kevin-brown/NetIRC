using System.IO;

namespace NetIRC.Messages.Send
{
    public class UserPrivate : ISendMessage
    {
        private readonly string _nick;
        private readonly string _message;

        public UserPrivate(string nick, string message)
        {
            this._nick = nick;
            this._message = message;
        }

        public UserPrivate(User user, string message)
        {
            this._nick = user.NickName;
            this._message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("PRIVMSG {0} :{1}", this._nick, this._message);
        }
    }
}
