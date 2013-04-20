using System;
using System.IO;

namespace NetIRC.Messages
{
    class NickMessage : Message
    {
        private User User;

        public NickMessage(User user)
        {
            this.User = user;
        }

        public void Send(StreamWriter writer)
        {
            writer.WriteLine("NICK " + this.User.NickName);
        }
    }
}
