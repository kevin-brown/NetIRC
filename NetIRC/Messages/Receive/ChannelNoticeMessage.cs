using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class ChannelNoticeMessage : ReceivePrivMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            if (!ReceiveUserMessage.CheckCommand(message, "NOTICE"))
            {
                return false;
            }

            if (ReceivePrivMessage.GetChannel(client, message) == null)
            {
                return false;
            }

            if (ReceivePrivMessage.IsCTCP(message))
            {
                return false;
            }

            return true;
        }

        public override void ProcessMessage(string message, Client client)
        {
            User user = ReceiveUserMessage.GetUser(client, message);

            Channel channel = ReceivePrivMessage.GetChannel(client, message);

            string[] parts = message.Split(' ');

            string notice = String.Join(" ", parts.Skip(3).ToArray()).Substring(1);

            channel.TriggerOnNotice(user, notice);
        }
    }
}
