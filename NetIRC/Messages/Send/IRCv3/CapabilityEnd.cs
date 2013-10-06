using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NetIRC.Messages.Send.IRCv3
{
    public class CapabilityEnd : ISendMessage
    {
        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("CAP END");
        }
    }
}
