using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class NickMessage : SendMessage
    {
        private User User;

        public NickMessage(string nick)
        {
            this.User = UserFactory.FromNick(nick);
        }

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
