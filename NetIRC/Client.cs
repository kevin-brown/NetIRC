using System;
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

        public Client()
        {

        }

        public void Connect()
        {

        }
    }
}
