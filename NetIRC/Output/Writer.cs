using System;

namespace NetIRC.Output
{
    public interface Writer
    {
        void ProcessSendMessage(string message, Client client);

        void ProcessReadMessage(string message, Client client);
    }
}
