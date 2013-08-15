using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class UserMessage : SendMessage
    {
        private User User;

        public UserMessage(User user)
        {
            this.User = user;
        }

        public void Send(StreamWriter writer)
        {
            writer.WriteLine("USER " + this.User.UserName + " 8 - :" + this.User.RealName);
        }

        public bool CheckMessage(string message, Client client)
        {
            throw new NotImplementedException();
        }
    }
}
