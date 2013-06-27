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

        internal StreamWriter Writer
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

        private List<Type> OutputWriters = new List<Type>();

        public Client()
        {
            this.RegisterMessages();
            this.RegisterWriters();
        }

        public Messages.Send.AwayMessage Away(string message)
        {
            return new Messages.Send.AwayMessage(message);
        }

        /// <summary>
        /// Connect to a specified <paramref name="server" />.
        /// </summary>
        /// <param name="server">The host of the server to connect to.</param>
        /// <param name="port">The port to connect to on the server.</param>
        /// <param name="ssl">True for SSL, false if not.</param>
        /// <param name="user">The NetIRC.User to use for connecting to the server.</param>
        public void Connect(string server, int port, bool ssl, User user)
        {
            this.User = UserFactory.FromNick(user.NickName);
            UserFactory.SetUser(user.NickName, user);
            this.User = UserFactory.FromNick(user.NickName);

            this.TcpClient = new TcpClient();
            this.TcpClient.Connect(server, port);

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
            this.Send(channel.Join());
            this.Send(channel.GetTopic());
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
            this.Send(channel.Part());
        }

        public Messages.Send.NotAwayMessage NotAway()
        {
            return new Messages.Send.NotAwayMessage();
        }

        public Messages.Send.QuitMessage Quit()
        {
            return new Messages.Send.QuitMessage();
        }

        public Messages.Send.QuitMessage Quit(string reason)
        {
            return new Messages.Send.QuitMessage(reason);
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

                foreach (Type writerType in this.OutputWriters)
                {
                    MethodInfo processMethod = writerType.GetMethod("ProcessReadMessage");
                    processMethod.Invoke(Activator.CreateInstance(writerType), new object[2] { line, this });
                }

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

        public void RegisterWriter(Type type)
        {
            this.OutputWriters.Add(type);
        }

        private void RegisterWriters()
        {
            this.OutputWriters.Add(typeof(Output.ConsoleWriter));
            this.OutputWriters.Add(typeof(Output.IrcWriter));
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

                foreach (Type writerType in this.OutputWriters)
                {
                    MethodInfo processMethod = writerType.GetMethod("ProcessSendMessage");
                    processMethod.Invoke(Activator.CreateInstance(writerType), new object[2] { line, this });
                }
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

        public void UnregisterMessage(Type type)
        {
            this.RegisteredMessages.Remove(type);
        }

        public void UnregisterWriter(Type type)
        {
            this.OutputWriters.Remove(type);
        }
    }
}
