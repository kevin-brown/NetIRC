using System;

namespace NetIRC.Messages
{
    public interface ReceiveMessage
    {
        bool CheckMessage(String message, Server server);
    }
}
