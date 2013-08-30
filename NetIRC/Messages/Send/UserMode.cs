using System.IO;

namespace NetIRC.Messages.Send
{
    public class UserMode : ISendMessage
    {
        private readonly string _nickName;
        private readonly string _modes;

        public UserMode(User user)
        {
            this._nickName = user.NickName;
        }

        public UserMode(User user, string modes)
        {
            this._nickName = user.NickName;
            this._modes = modes;
        }

        public UserMode(string nickName)
        {
            this._nickName = nickName;
        }

        public UserMode(string nickName, string modes)
        {
            this._nickName = nickName;
            this._modes = modes;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (string.IsNullOrEmpty(this._modes))
            {
                writer.WriteLine("MODE {0}", this._nickName);
            }

            else
            {
                writer.WriteLine("MODE {0} {1}", this._nickName, this._modes);
            }
        }
    }
}
