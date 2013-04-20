using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Send
{
    public class JoinMessage : SendMessage
    {
        private string channel;

        public JoinMessage(string channel)
        {
            this.channel = channel;
        }

        public void Send(System.IO.StreamWriter writer)
        {
            writer.WriteLine("JOIN " + this.channel);
        }
    }
}
