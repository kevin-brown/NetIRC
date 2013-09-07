namespace NetIRC.Messages.Receive
{
    public class ChannelPrivate : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "PRIVMSG" &&
                   message.IsChannel() &&
                   !message.IsCTCP();
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User user = message.GetUser();
            Channel channel = message.GetChannel();
            string line = message.Parameters[1];

            channel.TriggerOnMessage(user, line);
        }
    }
}
