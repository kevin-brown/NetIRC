using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive.CTCP
{
    class VersionReplyMessage : ReceivePrivMessage
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

            if (!ReceivePrivMessage.IsCTCP(message))
            {
                return false;
            }

            string[] parts = message.Split(' ');

            if (parts.Length < 5)
            {
                return false;
            }

            return ReceivePrivMessage.CheckCTCP(message, "VERSION");
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            User user = ReceiveUserMessage.GetUser(client, message);

            string version = String.Join(" ", parts.Skip(4).ToArray());
            version = version.Substring(0, version.Length - 1);

            client.User.TriggerOnVersionReply(user, version);
        }
    }
}
