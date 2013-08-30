using System;
using System.Linq;

namespace NetIRC.Messages.Receive.CTCP
{
    class VersionMessage : ReceivePrivMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "PRIVMSG" &&
                   !message.IsChannel() &&
                   message.IsCTCP() &&
                   message.GetCTCPCommand() == "VERSION";
        }

        public override void ProcessMessage(ParsedMessage message, Client client)
        {
            User target = message.GetUserFromNick(message.Parameters[0]);

            if (target == client.User)
            {
                User user = message.GetUser();

                client.TriggerOnVersion(user);
            }
        }
    }
}
