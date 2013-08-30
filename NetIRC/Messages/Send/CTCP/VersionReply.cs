using System.IO;

namespace NetIRC.Messages.Send.CTCP
{
    public class VersionReply : ISendMessage
    {
        private readonly User _user;
        private readonly string _version;

        public VersionReply(User user, string version)
        {
            this._user = user;
            this._version = version;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("NOTICE " + this._user.NickName + " \x001VERSION " + this._version + "\x001");
        }
    }
}
