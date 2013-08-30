using System;
using System.Linq;

namespace NetIRC.Messages.Receive
{
    class QuitMessage : ReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "QUIT";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User user = message.GetUser();
            string reason = message.Parameters[0];

            client.UserFactory.RemoveNick(user.UserName);
            user.TriggerOnQuit(reason);
            user._channels.Clear();
        }
    }
}
