using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class InviteMessage : ISendMessage
    {
        string channelName;
        string nickName;

        public InviteMessage(Channel channel, User user)
        {
            this.channelName = channel.FullName;
            this.nickName = user.NickName;
        }

        public InviteMessage(Channel channel, string nickName)
        {
            this.channelName = channel.FullName;
            this.nickName = nickName;
        }

        public InviteMessage(string channelName, User user)
        {
            this.channelName = channelName;
            this.nickName = user.NickName;
        }

        public InviteMessage(string channelName, string nickName)
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
