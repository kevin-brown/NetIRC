using System;
using System.IO;

namespace NetIRC.Messages
{
    public interface Message
    {
        void Send(StreamWriter writer);
    }
}
