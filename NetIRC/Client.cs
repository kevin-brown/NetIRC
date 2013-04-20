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
            this.TcpClient = new TcpClient();
            await this.TcpClient.ConnectAsync(server, port);

            this.Stream = this.TcpClient.GetStream();

            this.Reader = new StreamReader(this.Stream);
            this.Writer = new StreamWriter(this.Stream) { NewLine = "\r\n", AutoFlush = true };

            Thread readThread = new Thread(ReadStream);
            readThread.Start();

            this.Writer.WriteLine("USER test - frogbox.es :test");
            this.Writer.WriteLine("NICK test");
        }

        private void ReadStream()
        {
            while (this.TcpClient != null && this.TcpClient.Connected)
            {
                string line = this.Reader.ReadLine();

                if (String.IsNullOrEmpty(line))
                {
                    continue;
                }

                Console.WriteLine("[] < " + line);
            }
        }
    }
}
