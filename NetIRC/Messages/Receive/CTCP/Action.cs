namespace NetIRC.Messages.Receive.CTCP
{
    public class Action : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "PRIVMSG" &&
                   message.IsChannel() &&
                   message.IsCTCP() &&
                   message.GetCTCPCommand() == "ACTION" &&
                   message.HasCTCPParameter();
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User user = message.GetUser();
            Channel channel = message.GetChannel();
            string action = message.GetCTCPParameter();

            channel.TriggerOnAction(user, action);
        }
    }
}
