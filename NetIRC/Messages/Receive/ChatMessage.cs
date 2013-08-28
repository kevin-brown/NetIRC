using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class ChatMessage : ReceivePrivMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            return ReceiveUserMessage.CheckCommand(message, "PRIVMSG") &&
                (ReceivePrivMessage.GetChannel(client, message) != null) &&
                !ReceivePrivMessage.IsCTCP(message);
        }

        public override void ProcessMessage(string message, Client client)
        {
            User user = ReceiveUserMessage.GetUser(client, message);

            string[] parts = message.Split(' ');

            Channel channel = client.ChannelFactory.FromName(parts[2].Substring(1));

            string line = String.Join(" ", parts.Skip(3).ToArray()).Substring(1);

            channel.TriggerOnMessage(user, line);
        }
    }
}
