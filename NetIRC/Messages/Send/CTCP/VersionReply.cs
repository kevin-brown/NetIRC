using System.IO;

namespace NetIRC.Messages.Send.CTCP
{
    public class VersionReply : ISendMessage
    {
        private User user;
        private string version;

        public VersionReply(User user, string version)
        {
            this.user = user;
            this.version = version;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("NOTICE " + this.user.NickName + " \x001VERSION " + this.version + "\x001");
        }
    }
}
