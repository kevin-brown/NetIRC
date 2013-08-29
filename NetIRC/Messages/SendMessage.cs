using System;
using System.IO;

namespace NetIRC.Messages
{
    public interface SendMessage
    {
        void Send(StreamWriter writer, Client client);
    }
}
