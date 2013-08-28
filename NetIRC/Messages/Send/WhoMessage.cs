﻿using System;
using System.IO;

namespace NetIRC.Messages.Send
{
    public class WhoMessage : SendMessage
    {
        string target;

        public WhoMessage(string target)
        {
            this.target = target;
        }

        public WhoMessage(Channel channel)
        {
            this.target = channel.FullName;
        }

        public void Send(Client client, StreamWriter writer)
        {
            writer.WriteLine("WHO " + this.target);
        }
    }
}
