using System;
using System.Linq;

namespace NetIRC.Messages.Receive.CTCP
{
    class VersionMessage : ReceivePrivMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            if (!ReceiveUserMessage.CheckCommand(message, "PRIVMSG"))
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

            string[] parts = message.Split(' ');

            if (parts.Length > 4)
            {
                return false;
            }

            return ReceivePrivMessage.CheckCTCP(message, "VERSION");
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            User user = ReceiveUserMessage.GetUser(message);

            client.User.TriggerOnVersion(user);
        }
    }
}
