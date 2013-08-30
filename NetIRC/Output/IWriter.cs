using System;

namespace NetIRC.Output
{
    public interface IWriter
    {
        void ProcessSendMessage(string message, Client client);

        void ProcessReadMessage(string message, Client client);
    }
}
