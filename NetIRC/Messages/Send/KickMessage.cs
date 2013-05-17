using System;

namespace NetIRC.Messages.Send
{
    public class KickMessage : SendMessage
    {
        private Channel channel;
        private User user;

        public KickMessage(Channel channel, User user)
        {
            this.channel = channel;
            this.user = user;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("KICK #{0} {1]", channel.Name, user.NickName);
        }
    }
}
