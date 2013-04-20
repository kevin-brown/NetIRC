using System;
using System.IO;

namespace NetIRC.Messages
{
    public interface SendMessage : Message
    {
        void Send(StreamWriter writer);
    }
}
