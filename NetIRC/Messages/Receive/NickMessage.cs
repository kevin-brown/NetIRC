using System;

namespace NetIRC.Messages.Receive
{
    class NickMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            return ReceiveUserMessage.CheckCommand(message, "NICK");
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            string user = ReceiveUserMessage.GetUser(client, message).NickName;

            string nick = parts[2];

            User newUser = client.UserFactory.ChangeNick(user, nick);

            newUser.TriggerOnNickNameChange(user);
        }
    }
}
