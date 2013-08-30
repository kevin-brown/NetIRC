using System;
using System.Linq;

namespace NetIRC.Messages.Receive
{
    class QuitMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "QUIT";
        }

        public override void ProcessMessage(ParsedMessage message, Client client)
        {
            User user = message.GetUser();
            string reason = message.Parameters[0];

            client.UserFactory.RemoveNick(user.UserName);
            user.TriggerOnQuit(reason);
            user._channels.Clear();
        }
    }
}
