using System.IO;

namespace NetIRC.Messages.Send
{
    public class Invite : ISendMessage
    {
        readonly string _channelName;
        readonly string _nickName;

        public Invite(Channel channel, User user)
        {
            this._channelName = channel.FullName;
            this._nickName = user.NickName;
        }

        public Invite(Channel channel, string nickName)
        {
            this._channelName = channel.FullName;
            this._nickName = nickName;
        }

        public Invite(string channelName, User user)
        {
            this._channelName = channelName;
            this._nickName = user.NickName;
        }

        public Invite(string channelName, string nickName)
        {
            this._channelName = channelName;
            this._nickName = nickName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("INVITE {0} {1}", this._nickName, this._channelName);
        }
    }
}
