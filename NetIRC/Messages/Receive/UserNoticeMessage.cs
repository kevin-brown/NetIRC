using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetIRC.Messages.Receive
{
    class UserNoticeMessage : ReceivePrivMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            if (!ReceiveUserMessage.CheckCommand(message, "NOTICE"))
            {
                return false;
            }

            if (ReceivePrivMessage.GetChannel(client, message) != null)
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

            string[] parts = message.Split(' ');

            User user = ReceiveUserMessage.GetUser(client, message);
            string notice = String.Join(" ", parts.Skip(3).ToArray()).Substring(1);

            client.TriggerOnNotice(user, notice);
        }
    }
}
