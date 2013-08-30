using System.IO;

namespace NetIRC.Messages.Send.CTCP
{
    public class Action : ISendMessage
    {
        private readonly string _channelName;
        private readonly string _message;

        public Action(string channelName, string message)
        {
            this._channelName = channelName;
            this._message = message;
        }

        public Action(Channel channel, string message)
        {
            this._channelName = channel.FullName;
            this._message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("PRIVMSG {0} \x001" + "ACTION {1}\x001", this._channelName, this._message);

            client.ChannelFactory.FromName(this._channelName).TriggerOnSendAction(this._message);
        }
    }
}
