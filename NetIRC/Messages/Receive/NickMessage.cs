using System;

namespace NetIRC.Messages.Receive
{
    class NickMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            return ReceiveUserMessage.CheckCommand(message, "NICK");
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            User user = this.GetUser(message);

            string nick = parts[2];

            UserFactory.ChangeNick(user.NickName, nick);
        }
    }
}
