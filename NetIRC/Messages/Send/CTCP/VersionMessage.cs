using System;
using System.IO;

namespace NetIRC.Messages.Send.CTCP
{
    public class VersionMessage : SendMessage
    {
        private string nickName;

        public VersionMessage(string nickName)
        {
            this.nickName = nickName;
        }

        public VersionMessage(User user)
        {
            this.nickName = user.NickName;
        }

        public void Send(Client client, StreamWriter writer)
        {
            writer.WriteLine("PRIVMSG " + this.nickName + " \x001VERSION\x001");
        }
    }
}
