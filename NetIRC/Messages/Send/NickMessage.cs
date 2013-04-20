using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    class NickMessage : SendMessage
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

        public bool CheckMessage(string message, Server server)
        {
            throw new NotImplementedException();
        }
    }
}
