using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetIRC.Messages.Receive.CTCP
{
    class UserAction : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "PRIVMSG" &&
                   !message.IsChannel() &&
                   message.IsCTCP() &&
                   message.GetCTCPCommand() == "ACTION" &&
                   message.HasCTCPParameter();
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User target = message.GetUserFromNick(message.Parameters[0]);

            if (target == client.User)
            {
                User user = message.GetUser();
                string action = message.GetCTCPParameter();

                client.TriggerOnAction(user, action);
            }

        }
    }
}
