using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetIRC.Messages.Send.IRCv3
{
    public class CapabilityListSupported : ISendMessage
    {
        public void Send(System.IO.StreamWriter writer, Client client)
        {
            writer.WriteLine("CAP LS");
        }
    }
}
