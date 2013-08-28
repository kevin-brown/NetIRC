using System;
using System.IO;

namespace NetIRC.Messages
{
    public interface SendMessage
    {
        void Send(Client client, StreamWriter writer);
    }
}
