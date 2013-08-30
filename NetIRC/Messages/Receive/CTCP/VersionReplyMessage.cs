using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive.CTCP
{
    class VersionReplyMessage : ReceivePrivMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "NOTICE" &&
                   !message.IsChannel() &&
                   message.IsCTCP() &&
                   message.GetCTCPCommand() == "VERSION" &&
                   message.HasCTCPParameter();
        }

        public override void ProcessMessage(ParsedMessage message, Client client)
        {
            User target = message.GetUserFromNick(message.Parameters[0]);

            if (target == client.User)
            {
                User user = message.GetUser();
                string version = message.GetCTCPParameter();

                client.TriggerOnVersionReply(user, version);
            }
        }
    }
}
