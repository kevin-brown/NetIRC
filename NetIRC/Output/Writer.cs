using System;

namespace NetIRC.Output
{
    public interface Writer
    {
        void ProcessMessage(string message, Client client);
    }
}
