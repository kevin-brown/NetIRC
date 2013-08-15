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
            User user = ReceiveUserMessage.GetUser(message);
            string[] parts = message.Split(' ');

            string reason = String.Join(" ", parts.Skip(2).ToArray()).Substring(1);

            user.TriggerOnQuit(reason);

            user.Channels.Clear();
        }
    }
}
