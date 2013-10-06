using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NetIRC.IRCv3;

namespace NetIRC.Messages.Send.IRCv3
{
    public  class CapabilityRequest : ISendMessage
    {
        public string Capability { get; set; }

        public CapabilityRequest(string capability)
        {
            this.Capability = capability;
        }

        public CapabilityRequest(IEnumerable<string> capabilities)
        {
            this.Capability = String.Join(" ", capabilities);
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("CAP REQ :{0}", this.Capability);
        }
    }
}
