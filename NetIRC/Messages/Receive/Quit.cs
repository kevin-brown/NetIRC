namespace NetIRC.Messages.Receive
{
    public class Quit : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "QUIT";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User user = message.GetUser();
            string reason = message.Parameters[0];

            client.UserFactory.RemoveNick(user.NickName);
            user.TriggerOnQuit(reason);
            user._channels.Clear();
        }
    }
}
