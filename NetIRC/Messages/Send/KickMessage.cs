using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class KickMessage : ISendMessage
    {
        private string channelName;
        private string nickName;
        private string message;

        public KickMessage(Channel channel, User user)
        {
            this.channelName = channel.FullName;
            this.nickName = user.NickName;
        }

        public KickMessage(Channel channel, User user, string message)
        {
            this.channelName = channel.FullName;
            this.nickName = user.NickName;
            this.message = message;
        }

        public KickMessage(string channelName, User user)
        {
            this.channelName = channelName;
            this.nickName = user.NickName;
        }

        public KickMessage(string channelName, User user, string message)
        {
            this.channelName = channelName;
            this.nickName = user.NickName;
            this.message = message;
        }

        public KickMessage(Channel channel, string nickName)
        {
            this.channelName = channel.FullName;
            this.nickName = nickName;
        }

        public KickMessage(Channel channel, string nickName, string message)
        {
            this.channelName = channel.FullName;
            this.nickName = nickName;
            this.message = message;
        }

        public KickMessage(string channelName, string nickName)
        {
            this.channelName = channelName;
            this.nickName = nickName;
        }

        public KickMessage(string channelName, string nickName, string message)
        {
            this.channelName = channelName;
            this.nickName = nickName;
            this.message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (string.IsNullOrEmpty(this.message))
            {
                writer.WriteLine("KICK {0} {1}", this.channelName, this.nickName);
            }
            else
            {
                writer.WriteLine("KICK {0} {1} :{2}", this.channelName, this.nickName, this.message);
            }
        }
    }
}
