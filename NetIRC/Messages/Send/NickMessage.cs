using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class NickMessage : SendMessage
    {
        private string nickName;

        public NickMessage(string nickName)
        {
            this.nickName = nickName;
        }

        public NickMessage(User user)
        {
            this.nickName = user.NickName;
        }

        public void Send(Client client, StreamWriter writer)
        {
            writer.WriteLine("NICK " + this.nickName);
        }
    }
}
