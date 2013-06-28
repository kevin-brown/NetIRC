using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Send
{
    public class UserModeMessage : SendMessage
    {
        private User user;
        private string modes;

        public UserModeMessage(User user)
        {
            this.user = user;
        }

        public UserModeMessage(User user, string modes)
        {
            this.user = user;
            this.modes = modes;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            if (string.IsNullOrEmpty(modes))
            {
                writer.WriteLine("MODE {0}", user.NickName);
            }

            else
            {
                writer.WriteLine("MODE {0} {1}", user.NickName, modes);
            }
        }
    }
}
