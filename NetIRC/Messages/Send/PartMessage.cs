using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Send
{
    public class PartMessage : SendMessage
    {
        private string channel;

        public PartMessage(string channel)
        {
            this.channel = channel;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("PART " + this.channel);
        }
    }
}
