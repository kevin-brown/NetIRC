using System.IO;

namespace NetIRC.Messages.Send
{
    public class Invite : ISendMessage
    {
        public string ChannelName { get; set; }
        public string NickName { get; set; }

        public Invite(Channel channel, User user)
        {
            this.ChannelName = channel.FullName;
            this.NickName = user.NickName;
        }

        public Invite(Channel channel, string nickName)
        {
            this.ChannelName = channel.FullName;
            this.NickName = nickName;
        }

        public Invite(string channelName, User user)
        {
            this.ChannelName = channelName;
            this.NickName = user.NickName;
        }

        public Invite(string channelName, string nickName)
        {
            this.ChannelName = channelName;
            this.NickName = nickName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("INVITE {0} {1}", this.NickName, this.ChannelName);
        }
    }
}
