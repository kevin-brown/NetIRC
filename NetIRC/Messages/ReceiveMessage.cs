using System;

namespace NetIRC.Messages
{
    public interface ReceiveMessage
    {
        void ProcessMessage(String message, Client client);
    }
}
