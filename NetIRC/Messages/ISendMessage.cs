using System;
using System.IO;

namespace NetIRC.Messages
{
    public interface ISendMessage
    {
        void Send(StreamWriter writer, Client client);
    }
}
