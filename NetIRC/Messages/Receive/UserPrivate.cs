namespace NetIRC.Messages.Receive
{
    public class UserPrivate : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "PRIVMSG" &&
                   !message.IsChannel() &&
                   !message.IsCTCP();
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User target = message.GetUserFromNick(message.Parameters[0]);

            if (target == client.User)
            {
                User user = message.GetUser();
                string line = message.Parameters[1];

                client.TriggerOnMessage(user, line);
            }
        }
    }
}
