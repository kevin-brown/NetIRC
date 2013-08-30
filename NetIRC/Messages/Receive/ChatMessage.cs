using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class ChatMessage : ReceivePrivMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "PRIVMSG" &&
                   message.IsChannel() &&
                   !message.IsCTCP();
        }

        public override void ProcessMessage(ParsedMessage message, Client client)
        {
            User user = message.GetUser();
            Channel channel = message.GetChannel();
            string line = message.Parameters[1];

            channel.TriggerOnMessage(user, line);
        }
    }
}
