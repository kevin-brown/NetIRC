using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace NetIRC
{
    public class Client
    {
        private TcpClient TcpClient
        {
            get;
            set;
        }

        private NetworkStream Stream
        {
            get;
            set;
        }

        private StreamReader Reader
        {
            get;
            set;
        }

        private StreamWriter Writer
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

        public async void Connect(string server, int port, bool ssl)
        {
            this.Client = new TcpClient();
            await this.Client.ConnectAsync(server, port);

            this.Stream = this.Client.GetStream();

            this.Reader = new StreamReader(this.Stream);
            this.Writer = new StreamWriter(this.Stream);

            Thread readThread = new Thread(ReadStream);
        }

        private async void ReadStream()
        {
            while (this.Client != null && this.Client.Connected)
            {
                string line = await this.Reader.ReadLineAsync();

                Console.WriteLine(line);
            }
        }
    }
}
