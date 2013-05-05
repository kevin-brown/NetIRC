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

        private Thread ReadThread
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
            get
            {
                return ChannelFactory.HasUser(UserFactory.FromNick(this.User.NickName));
            }
        }

        public User User
        {
            get;
            private set;
        }

        private List<Type> RegisteredMessages = new List<Type>();

        public Client()
        {
            this.RegisterMessages();
        }

        /// <summary>
        /// Connect to a specified <paramref name="server" />.
        /// </summary>
        /// <param name="server">The host of the server to connect to.</param>
        /// <param name="port">The port to connect to on the server.</param>
        /// <param name="ssl">True for SSL, false if not.</param>
        /// <param name="user">The NetIRC.User to use for connecting to the server.</param>
        public async void Connect(string server, int port, bool ssl, User user)
        {
            this.User = UserFactory.FromNick(user.NickName);
            UserFactory.SetUser(user.NickName, user);
            this.User = UserFactory.FromNick(user.NickName);

            this.TcpClient = new TcpClient();
            await this.TcpClient.ConnectAsync(server, port);

            this.Server = new Server(server, port);
            this.Stream = this.TcpClient.GetStream();

            this.Reader = new StreamReader(this.Stream);
            this.Writer = new StreamWriter(this.Stream) { NewLine = "\r\n", AutoFlush = true };

            this.ReadThread = new Thread(ReadStream);
            this.ReadThread.Start();

            this.Send(new Messages.Send.UserMessage(this.User));
            this.Send(new Messages.Send.NickMessage(this.User));
        }

        /// <summary>
        /// Disconnects the connected user forcefully.
        /// </summary>
        public void Disconnect()
        {
            this.ReadThread.Abort();

            this.Reader.Close();
            this.Writer.Close();
            this.TcpClient.Close();
        }

        /// <summary>
        /// Join a channel specified by the <paramref name="name" />.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        public void JoinChannel(string name)
        {
            this.JoinChannel(ChannelFactory.FromName(name));
        }

        /// <summary>
        /// Join a specified channel based on the given Channel object.
        /// </summary>
        /// <param name="channel">The Channel to be used for connecting.</param>
        public void JoinChannel(Channel channel)
        {
            this.Send(new Messages.Send.JoinMessage("#" + channel.Name));
            this.Send(new Messages.Send.TopicMessage("#" + channel.Name));
        }

        /// <summary>
        /// Leave a channel specified by the <paramref name="name" />.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        public void LeaveChannel(string name)
        {
            this.LeaveChannel(ChannelFactory.FromName(name));
        }

        /// <summary>
        /// Leave a specified channel based on the given Channel object.
        /// </summary>
        /// <param name="channel"></param>
        public void LeaveChannel(Channel channel)
        {
            this.Send(new Messages.Send.PartMessage("#" + channel.Name));
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

        /// <summary>
        /// Registers a message that will be checked when new messages are received.
        /// </summary>
        /// <param name="type">The type of the Message object that messages will be checked against.</param>
        public void RegisterMessage(Type type)
        {
            this.RegisteredMessages.Add(type);
        }

        private void RegisterMessages()
        {
            this.RegisteredMessages.Add(typeof(Messages.Receive.PingMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.JoinMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.PartMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.NickMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.TopicMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.QuitMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.KickMessage));

            this.RegisteredMessages.Add(typeof(Messages.Receive.ChatMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.NoticeMessage));

            this.RegisteredMessages.Add(typeof(Messages.Receive.CTCP.ActionMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.CTCP.VersionMessage));

            this.RegisteredMessages.Add(typeof(Messages.Receive.CTCP.VersionReplyMessage));

            this.RegisteredMessages.Add(typeof(Messages.Receive.Numerics.WelcomeMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.Numerics.NamesMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.Numerics.WhoMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.Numerics.TopicMessage));
            this.RegisteredMessages.Add(typeof(Messages.Receive.Numerics.TopicInfo));
            this.RegisteredMessages.Add(typeof(Messages.Receive.Numerics.NoTopic));
        }

        /// <summary>
        /// Send a message to the connected server.
        /// </summary>
        /// <param name="message">The NetIRC.SendMessage instance to be sent.</param>
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

        #region Events

        public delegate void OnChannelJoinHandler(Client client, Channel channel);
        public event OnChannelJoinHandler OnChannelJoin;

        internal void TriggerOnChannelJoin(Channel channel)
        {
            if (OnChannelJoin != null)
            {
                OnChannelJoin(this, channel);
            }
        }

        public delegate void OnChannelLeaveHandler(Client client, Channel channel);
        public event OnChannelLeaveHandler OnChannelLeave;

        internal void TriggerOnChannelLeave(Channel channel)
        {
            if (OnChannelLeave != null)
            {
                OnChannelLeave(this, channel);
            }
        }

        public delegate void OnConnectHandler(Client client);
        public event OnConnectHandler OnConnect;

        internal void TriggerOnConnect()
        {
            if (OnConnect != null)
            {
                OnConnect(this);
            }
        }

        #endregion
    }
}
