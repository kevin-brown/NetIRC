using System;
using System.IO;

namespace NetIRC.Messages.Send.CTCP
{
    public class Version : ISendMessage
    {
        private string nickName;

        public Version(string nickName)
        {
            this.nickName = nickName;
        }

        public Version(User user)
        {
            this.nickName = user.NickName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("PRIVMSG " + this.nickName + " \x001VERSION\x001");
        }
    }
}
