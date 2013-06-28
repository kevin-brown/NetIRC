using System;
using System.Linq;

namespace NetIRC.Messages.Receive.CTCP
{
    class ActionMessage : ReceivePrivMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            if (!ReceiveUserMessage.CheckCommand(message, "PRIVMSG"))
            {
                return false;
            }

            if (ReceivePrivMessage.GetChannel(message) == null)
            {
                return false;
            }

            if (!ReceivePrivMessage.IsCTCP(message))
            {
                return false;
            }
            
            return ReceivePrivMessage.CheckCTCP(message, "ACTION");
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            string action = String.Join(" ", parts.Skip(4).ToArray());
            action = action.Substring(0, action.Length - 1);

            Channel channel = ReceivePrivMessage.GetChannel(message);
            User user = ReceiveUserMessage.GetUser(message);

            channel.TriggerOnAction(user, action);
        }
    }
}
