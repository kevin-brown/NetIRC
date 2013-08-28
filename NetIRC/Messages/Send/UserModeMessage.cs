using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Send
{
    public class UserModeMessage : SendMessage
    {
        private string nickName;
        private string modes;

        public UserModeMessage(User user)
        {
            this.nickName = user.NickName;
        }

        public UserModeMessage(User user, string modes)
        {
            this.nickName = user.NickName;
            this.modes = modes;
        }

        public UserModeMessage(string nickName)
        {
            this.nickName = nickName;
        }

        public UserModeMessage(string nickName, string modes)
        {
            this.nickName = nickName;
            this.modes = modes;
        }

        public void Send(Client client, StreamWriter writer)
        {
            if (string.IsNullOrEmpty(this.modes))
            {
                writer.WriteLine("MODE {0}", this.nickName);
            }

            else
            {
                writer.WriteLine("MODE {0} {1}", this.nickName, this.modes);
            }
        }
    }
}
