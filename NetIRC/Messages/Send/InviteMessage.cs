using System;

namespace NetIRC.Messages.Send
{
    public class InviteMessage : SendMessage
    {
        Channel channel;
        User user;

        public InviteMessage(Channel channel, User user)
        {
            this.channel = channel;
            this.user = user;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("INVITE {0} {1}{2}", this.user.NickName, Channel.TypeChars[this.channel.Type], this.channel.Name);
        }
    }
}
