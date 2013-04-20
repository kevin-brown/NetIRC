using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace NetIRC
{
    public class Client
    {
        private TcpClient Client
        {
            get;
            set;
        }

        public Server Server
        {
            get;
            private set;
        }

        public List<Channel> Channels
        {
            get;
            private set;
        }

        public Client()
        {

        }

        public void Connect()
        {

        }
    }
}
