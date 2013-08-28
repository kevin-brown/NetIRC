using System;
using System.IO;

namespace NetIRC.Messages.Send.CTCP
{
    public class ActionMessage : SendMessage
    {
        private string channelName;
        private string message;

        public ActionMessage(string channelName, string message)
        {
            this.channelName = channelName;
            this.message = message;
        }

        public ActionMessage(Channel channel, string message)
        {
            this.channelName = channel.FullName;
            this.message = message;
        }

        public void Send(Client client, StreamWriter writer)
        {
            writer.WriteLine("PRIVMSG {0} \x001" + "ACTION {1}\x001", this.channelName, this.message);

            client.ChannelFactory.FromName(this.channelName).TriggerOnSendAction(message);
        }
    }
}
