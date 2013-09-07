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

            channel.LeaveUser(user);

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
