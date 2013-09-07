using System.IO;

namespace NetIRC.Messages.Send
{
    public class Kick : ISendMessage
    {
        public string ChannelName { get; set; }
        public string NickName { get; set; }
        public string Message { get; set; }

        public Kick(Channel channel, User user)
        {
            this.ChannelName = channel.FullName;
            this.NickName = user.NickName;
        }

        public Kick(Channel channel, User user, string message)
        {
            this.ChannelName = channel.FullName;
            this.NickName = user.NickName;
            this.Message = message;
        }

        public Kick(string channelName, User user)
        {
            this.ChannelName = channelName;
            this.NickName = user.NickName;
        }

        public Kick(string channelName, User user, string message)
        {
            this.ChannelName = channelName;
            this.NickName = user.NickName;
            this.Message = message;
        }

        public Kick(Channel channel, string nickName)
        {
            this.ChannelName = channel.FullName;
            this.NickName = nickName;
        }

        public Kick(Channel channel, string nickName, string message)
        {
            this.ChannelName = channel.FullName;
            this.NickName = nickName;
            this.Message = message;
        }

        public Kick(string channelName, string nickName)
        {
            this.ChannelName = channelName;
            this.NickName = nickName;
        }

        public Kick(string channelName, string nickName, string message)
        {
            this.ChannelName = channelName;
            this.NickName = nickName;
            this.Message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (string.IsNullOrEmpty(this.Message))
            {
                writer.WriteLine("KICK {0} {1}", this.ChannelName, this.NickName);
            }
            else
            {
                writer.WriteLine("KICK {0} {1} :{2}", this.ChannelName, this.NickName, this.Message);
            }
        }
    }
}
