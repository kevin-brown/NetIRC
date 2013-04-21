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

            string user = this.GetUser(message).NickName;

            string nick = parts[2];

            UserFactory.ChangeNick(user, nick);

            User newUser = UserFactory.FromNick(nick);

            newUser.TriggerOnNickChange(user);
        }
    }
}
