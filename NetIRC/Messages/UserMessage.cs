using System;
using System.IO;

namespace NetIRC.Messages
{
    class UserMessage : Message
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
    }
}
