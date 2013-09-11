namespace NetIRC.Messages.Receive
{
    public class Part : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "PART";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            Channel channel = message.GetChannel();
            User user = message.GetUser();

            string reason = null;
            if (message.Parameters.Length >= 2)
                reason = message.Parameters[1];

            channel.LeaveUser(user, reason);

            if (user == client.User)
            {
                client.ChannelFactory.RemoveName(channel.Name);
                client.TriggerOnChannelLeave(channel);
                channel.ClearUsers();
            }

            client.Send(channel.SendWho());
        }
    }
}
