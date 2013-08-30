namespace NetIRC.Messages.Receive
{
    class Kick : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "KICK";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User kicker = message.GetUser();
            Channel channel = message.GetChannel();
            User user = message.GetUserFromNick(message.Parameters[1]);
            string reason = message.Parameters[2];

            channel.TriggerOnKick(kicker, user, reason);
        }
    }
}
