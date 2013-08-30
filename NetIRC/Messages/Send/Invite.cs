using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class Invite : ISendMessage
    {
        string channelName;
        string nickName;

        public Invite(Channel channel, User user)
        {
            this.channelName = channel.FullName;
            this.nickName = user.NickName;
        }

        public Invite(Channel channel, string nickName)
        {
            this.channelName = channel.FullName;
            this.nickName = nickName;
        }

        public Invite(string channelName, User user)
        {
            this.channelName = channelName;
            this.nickName = user.NickName;
        }

        public Invite(string channelName, string nickName)
        {
            this.channelName = channelName;
            this.nickName = nickName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("INVITE {0} {1}", this.nickName, this.channelName);
        }
    }
}
