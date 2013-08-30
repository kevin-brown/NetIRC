using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class Nick : ISendMessage
    {
        private string nickName;

        public Nick(string nickName)
        {
            this.nickName = nickName;
        }

        public Nick(User user)
        {
            this.nickName = user.NickName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("NICK " + this.nickName);
        }
    }
}
