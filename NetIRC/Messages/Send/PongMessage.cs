﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Send
{
    public class PongMessage : SendMessage
    {
        private string extra;

        public PongMessage(string extra)
        {
            this.extra = extra;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("PONG {0}", this.extra);
        }
    }
}
