using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class NoticeMessage : ReceivePrivMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            if (!ReceiveUserMessage.CheckCommand(message, "NOTICE"))
            {
                return false;
            }

            if (ReceivePrivMessage.GetChannel(message) == null)
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
            User user = ReceiveUserMessage.GetUser(message);

            Channel channel = ReceivePrivMessage.GetChannel(message);

            string[] parts = message.Split(' ');

            string notice = String.Join(" ", parts.Skip(3).ToArray()).Substring(1);

            channel.TriggerOnNotice(user, notice);
        }
    }
}
