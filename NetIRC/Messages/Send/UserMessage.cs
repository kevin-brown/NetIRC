using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    class UserMessage : SendMessage
    {
        private User User;

        public UserMessage(User user)
        {
            this.User = user;
        }

        public void Send(StreamWriter writer)
        {
            writer.WriteLine("USER " + this.User.UserName + " - - :" + this.User.RealName);
        }

        public bool CheckMessage(string message, Server server)
        {
            throw new NotImplementedException();
        }
    }
}
