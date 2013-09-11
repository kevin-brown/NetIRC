namespace NetIRC.Messages.Receive.Numerics
{
    public class Topic : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "332";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User target = message.GetUserFromNick(message.Parameters[0]);

            if (target == client.User)
            {
                Channel channel = message.GetChannel(message.Parameters[1]);
                
                string topic = null;
                if (message.Parameters.Length > 3)
                    topic= message.Parameters[2];

                channel.Topic.Message = topic;
            }

        }
    }
}
