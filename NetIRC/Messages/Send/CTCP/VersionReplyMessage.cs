using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Send.CTCP
{
    public class VersionReplyMessage : SendMessage
    {
        private User user;
        private string version;

        public VersionReplyMessage(User user, string version)
        {
            this.user = user;
            this.version = version;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("NOTICE " + this.user.NickName + " \x001VERSION " + this.version + "\x001");
        }
    }
}
