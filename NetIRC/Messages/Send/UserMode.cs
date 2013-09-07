using System.IO;

namespace NetIRC.Messages.Send
{
    public class UserMode : ISendMessage
    {
        public string NickName { get; set; }
        public string Modes { get; set; }

        public UserMode(User user)
        {
            this.NickName = user.NickName;
        }

        public UserMode(User user, string modes)
        {
            this.NickName = user.NickName;
            this.Modes = modes;
        }

        public UserMode(string nickName)
        {
            this.NickName = nickName;
        }

        public UserMode(string nickName, string modes)
        {
            this.NickName = nickName;
            this.Modes = modes;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (string.IsNullOrEmpty(this.Modes))
            {
                writer.WriteLine("MODE {0}", this.NickName);
            }

            else
            {
                writer.WriteLine("MODE {0} {1}", this.NickName, this.Modes);
            }
        }
    }
}
