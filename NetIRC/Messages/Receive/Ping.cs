namespace NetIRC.Messages.Receive
{
    class Ping : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "PING";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            string extra = message.Parameters[0];

            client.Send(new Messages.Send.Pong(extra));
        }
    }
}
