using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
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

        public Dictionary<string, Channel> Channels
        {
            get;
            private set;
        }

        private ClientUser User
        {
            get;
            set;
        }

        private List<Type> RegisteredMessages = new List<Type>();

        public Client()
        {
            this.Channels = new Dictionary<string, Channel>();

            this.RegisterMessages();
        }

        private void RegisterMessages()
        {
            this.RegisteredMessages.Add(typeof(Messages.Receive.PingMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.Numerics.WelcomeMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.JoinMessage));
        }

        public async void Connect(string server, int port, bool ssl, ClientUser user)
        {
            this.User = user;

            this.TcpClient = new TcpClient();
            await this.TcpClient.ConnectAsync(server, port);

            this.Server = new Server(server, port);
            this.Stream = this.TcpClient.GetStream();

            this.Reader = new StreamReader(this.Stream);
            this.Writer = new StreamWriter(this.Stream) { NewLine = "\r\n", AutoFlush = true };

            Thread readThread = new Thread(ReadStream);
            readThread.Start();

            this.Send(new Messages.Send.UserMessage(this.User));
            this.Send(new Messages.Send.NickMessage(this.User));
        }

        public void JoinChannel(string name)
        {
            this.Send(new Messages.Send.JoinMessage("#" + name));

            this.Channels.Add(name, new Channel(name));
        }

        private void ReadStream()
        {
            List<string> messageList = new List<string>();

            while (this.TcpClient != null && this.TcpClient.Connected)
            {
                string line = this.Reader.ReadLine();

                if (String.IsNullOrEmpty(line))
                {
                    continue;
                }

                Console.WriteLine(string.Format("[{0:HH:mm:ss}] < {1}", DateTime.Now, line));

                foreach (Type messageType in this.RegisteredMessages)
                {
                    MethodInfo checkMessage = messageType.GetMethod("CheckMessage");
                    bool shouldProcess = (bool)checkMessage.Invoke(null, new object[2] {line, this.Server});

                    if (shouldProcess)
                    {
                        MethodInfo processMessage = messageType.GetMethod("ProcessMessage");
                        processMessage.Invoke(Activator.CreateInstance(messageType), new object[2] { line, this });
                    }
                }
            }
        }

        public void Send(Messages.SendMessage message)
        {
            MemoryStream stream = new MemoryStream();

            message.Send(new StreamWriter(stream) { AutoFlush = true });

            StreamReader reader = new StreamReader(stream);
            stream.Position = 0;

            while (true)
            {
                string line = reader.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                Console.WriteLine(string.Format("[{0:HH:mm:ss}] > {1}", DateTime.Now, line));
                this.Writer.WriteLine(line);
            }

            stream.Close();
        }
    }
}
