using System;

namespace NetIRC.Messages
{
    public interface ReceiveMessage
    {
        void ProcessMessage(ParsedMessage message, Client client);
    }
}
