using System;
using System.Linq;

namespace NetIRC.Messages.Receive.CTCP
{
    class VersionMessage : ReceivePrivMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            if (!ReceiveUserMessage.CheckCommand(message, "PRIVMSG") &&
                !ReceiveUserMessage.CheckCommand(message, "NOTICE"))
            {
                return false;
            }

            if (ReceivePrivMessage.GetChannel(message) != null)
            {
                return false;
            }

            if (!ReceivePrivMessage.IsCTCP(message))
            {
                return false;
            }

            return ReceivePrivMessage.CheckCTCP(message, "VERSION");
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            User user = this.GetUser(message);

            client.User.TriggerOnVersion(user);
        }
    }
}
