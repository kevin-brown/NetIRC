using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NetIRC.Messages.Send
{
    public class PrivateMessage : SendMessage
    {
        private string nick;
        private string message;

        public PrivateMessage(string nick, string message)
        {
            this.nick = nick;
            this.message = message;
        }

        public PrivateMessage(User user, string message)
        {
            this.nick = user.NickName;
            this.message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("PRIVMSG {0} :{1}", this.nick, this.message);
        }
    }
}
