using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class ChatMessage : ReceivePrivMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            return ReceiveUserMessage.CheckCommand(message, "PRIVMSG") &&
                (ReceivePrivMessage.GetChannel(message) != null) &&
                !ReceivePrivMessage.IsCTCP(message);
        }

        public override void ProcessMessage(string message, Client client)
        {
            User user = this.GetUser(message);

            string[] parts = message.Split(' ');

            Channel channel = ChannelFactory.FromName(parts[2].Substring(1));

            string line = String.Join(" ", parts.Skip(3).ToArray()).Substring(1);

            channel.TriggerOnMessage(user, line);
        }
    }
}
