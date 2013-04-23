using System;
using System.Linq;

namespace NetIRC.Messages.Receive
{
    class QuitMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            return ReceiveUserMessage.CheckCommand(message, "QUIT");
        }

        public override void ProcessMessage(string message, Client client)
        {
            User user = this.GetUser(message);
            string[] parts = message.Split(' ');

            string reason = String.Join(" ", parts.Skip(2).ToArray()).Substring(1);

            user.TriggerOnQuit(reason);
        }
    }
}
