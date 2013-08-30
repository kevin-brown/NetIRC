using System.IO;

namespace NetIRC.Messages.Send.CTCP
{
    public class Version : ISendMessage
    {
        private readonly string _nickName;

        public Version(string nickName)
        {
            this._nickName = nickName;
        }

        public Version(User user)
        {
            this._nickName = user.NickName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("PRIVMSG " + this._nickName + " \x001VERSION\x001");
        }
    }
}
