using System;

namespace NetIRC
{
    class Server
    {
        public string HostName
        {
            get;
            private set;
        }

        public int Port
        {
            get;
            private set;
        }

        public Server(string hostName, int port)
        {
            this.HostName = hostName;
            this.Port = port;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", this.HostName, this.Port);
        }
    }
}
