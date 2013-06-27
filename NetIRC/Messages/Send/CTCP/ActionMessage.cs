using System;

namespace NetIRC.Messages.Send.CTCP
{
    public class ActionMessage : SendMessage
    {
        private Channel channel;
        private string message;

        public ActionMessage(string channel, string message)
        {
            this.channel = ChannelFactory.FromName(channel);
            this.message = message;
        }

        public ActionMessage(Channel channel, string message)
        {
            this.channel = channel;
            this.message = message;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("PRIVMSG #{0} \x001" + "ACTION {1}\x001", this.channel.Name, this.message);
            this.channel.TriggerOnSendAction(this.message);
        }
    }
}
