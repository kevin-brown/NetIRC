namespace NetIRC.Messages.Receive.Numerics
{
    public class NoTopic : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "331";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User target = message.GetUserFromNick(message.Parameters[0]);

            if (target == client.User)
            {
                Channel channel = message.GetChannel(message.Parameters[1]);

                channel.Topic.ClearTopic();
            }
        }
    }
}
