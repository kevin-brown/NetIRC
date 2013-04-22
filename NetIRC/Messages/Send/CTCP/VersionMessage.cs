using System;

namespace NetIRC.Messages.Send.CTCP
{
    public class VersionMessage : SendMessage
    {
        private User user;

        public VersionMessage(User user)
        {
            this.user = user;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("PRIVMSG " + this.user.NickName + " \x001VERSION\x001");
        }
    }
}
