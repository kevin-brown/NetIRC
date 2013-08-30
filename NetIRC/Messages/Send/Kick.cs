using System.IO;

namespace NetIRC.Messages.Send
{
    public class Kick : ISendMessage
    {
        private readonly string _channelName;
        private readonly string _nickName;
        private readonly string _message;

        public Kick(Channel channel, User user)
        {
            this._channelName = channel.FullName;
            this._nickName = user.NickName;
        }

        public Kick(Channel channel, User user, string message)
        {
            this._channelName = channel.FullName;
            this._nickName = user.NickName;
            this._message = message;
        }

        public Kick(string channelName, User user)
        {
            this._channelName = channelName;
            this._nickName = user.NickName;
        }

        public Kick(string channelName, User user, string message)
        {
            this._channelName = channelName;
            this._nickName = user.NickName;
            this._message = message;
        }

        public Kick(Channel channel, string nickName)
        {
            this._channelName = channel.FullName;
            this._nickName = nickName;
        }

        public Kick(Channel channel, string nickName, string message)
        {
            this._channelName = channel.FullName;
            this._nickName = nickName;
            this._message = message;
        }

        public Kick(string channelName, string nickName)
        {
            this._channelName = channelName;
            this._nickName = nickName;
        }

        public Kick(string channelName, string nickName, string message)
        {
            this._channelName = channelName;
            this._nickName = nickName;
            this._message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (string.IsNullOrEmpty(this._message))
            {
                writer.WriteLine("KICK {0} {1}", this._channelName, this._nickName);
            }
            else
            {
                writer.WriteLine("KICK {0} {1} :{2}", this._channelName, this._nickName, this._message);
            }
        }
    }
}
