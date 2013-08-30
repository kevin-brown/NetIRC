using System;

namespace NetIRC.Messages.Receive
{
    class NickMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "NICK";
        }

        public override void ProcessMessage(ParsedMessage message, Client client)
        {
            string user = message.GetUser().NickName;
            string nick = message.Parameters[0];
            User newUser = client.UserFactory.ChangeNick(user, nick);

            newUser.TriggerOnNickNameChange(user);
        }
    }
}
