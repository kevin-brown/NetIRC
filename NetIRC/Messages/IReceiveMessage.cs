namespace NetIRC.Messages
{
    public interface IReceiveMessage
    {
        void ProcessMessage(ParsedMessage message, Client client);
    }
}
