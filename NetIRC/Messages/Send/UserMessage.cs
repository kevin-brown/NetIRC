using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class UserMessage : SendMessage
    {
        private string userName;
        private string realName;

        public UserMessage(string userName, string realName)
        {
            this.userName = userName;
            this.realName = realName;
        }

        public UserMessage(User user)
        {
            this.userName = user.UserName;
            this.realName = user.RealName;
        }

        public void Send(Client client, StreamWriter writer)
        {
            //TODO: allow mode to be set
            writer.WriteLine("USER " + this.userName + " 0 - :" + this.realName);
        }
    }
}
