using System;

namespace NetIRC.Messages.Send
{
    public class KickMessage : SendMessage
    {
        private Channel channel;
        private User user;
        private string message;

        public KickMessage(Channel channel, User user)
        {
            this.channel = channel;
            this.user = user;
        }

        public KickMessage(Channel channel, User user, string message)
        {
            this.channel = channel;
            this.user = user;
            this.message = message;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            if (string.IsNullOrEmpty(this.message))
            {
                writer.WriteLine("KICK #{0} {1}", channel.Name, user.NickName);
            }
            else
            {
                writer.WriteLine("KICK #{0} {1} {2}", channel.Name, user.NickName, this.message);
            }
        }
    }
}
