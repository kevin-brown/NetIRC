using System;
using System.Linq;

namespace NetIRC.Messages.Receive
{
    class QuitMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            return ReceiveUserMessage.CheckCommand(message, "QUIT");
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            string user = ReceiveUserMessage.GetUser(client, message).NickName;

            string reason = String.Join(" ", parts.Skip(2).ToArray()).Substring(1);

            User newUser = client.UserFactory.RemoveNick(user);

            newUser.TriggerOnQuit(reason);
            newUser._channels.Clear();
        }
    }
}
