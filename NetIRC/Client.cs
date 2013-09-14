using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using NetIRC.Messages;
using NetIRC.Output;

namespace NetIRC
{
    public class Client
    {
        private readonly List<RegisteredMessage> _registeredMessages = new List<RegisteredMessage>();
        private readonly List<Type> _outputWriters = new List<Type>();

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

        /// <summary>
        /// The hostname of the server.
        /// </summary>
        public string HostName
        {
            get;
            private set;
        }

        /// <summary>
        /// The port of the server.
        /// </summary>
        public int Port
        {
            get;
            private set;
        }

        public Dictionary<string, Channel> Channels
        {
            get
            {
                return this.ChannelFactory.HasUser(this.User);
            }
        }

        internal ChannelFactory ChannelFactory
        {
            get;
            private set;
        }

        internal UserFactory UserFactory
        {
            get;
            private set;
        }

        public User User
        {
            get;
            private set;
        }

        public Client()
        {
            this.UserFactory = new UserFactory(this);
            this.ChannelFactory = new ChannelFactory(this);

            this.RegisterMessages();
            this.RegisterWriters();
        }

        public Messages.Send.Away Away(string message)
        {
            return new Messages.Send.Away(message);
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
            this.User = user;
            this.UserFactory.SetUser(user.NickName, user);

            this.TcpClient = new TcpClient();
            this.TcpClient.Connect(server, port);

            this.HostName = server;
            this.Port = port;

            this.Stream = this.TcpClient.GetStream();

            this.Reader = new StreamReader(this.Stream);
            this.Writer = new StreamWriter(this.Stream) { NewLine = "\r\n", AutoFlush = true };

            this.ReadThread = new Thread(this.ReadStream) {IsBackground = true};
            this.ReadThread.Start();

            this.Send(new Messages.Send.UserMessage(this.User));
            this.Send(new Messages.Send.Nick(this.User));
        }

        /// <summary>
        /// Disconnects the connected user forcefully.
        /// </summary>
        public void Disconnect()
        {
            this.Reader.Close();
            this.Writer.Close();
            this.TcpClient.Close();

            this.Reader = null;
        }

        /// <summary>
        /// Join a channel specified by the <paramref name="name" />.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        public void JoinChannel(string name)
        {
            this.JoinChannel(new Channel(name));
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
            this.LeaveChannel(new Channel(name));
        }

        /// <summary>
        /// Leave a specified channel based on the given Channel object.
        /// </summary>
        /// <param name="channel"></param>
        public void LeaveChannel(Channel channel)
        {
            this.Send(channel.Part());
        }

        public Messages.Send.NotAway NotAway()
        {
            return new Messages.Send.NotAway();
        }

        public Messages.Send.Quit Quit()
        {
            return new Messages.Send.Quit();
        }

        public Messages.Send.Quit Quit(string reason)
        {
            return new Messages.Send.Quit(reason);
        }

        private void ReadStream()
        {
            while (this.Reader != null && !this.Reader.EndOfStream)
            {
                string line = this.Reader.ReadLine();
                if (String.IsNullOrEmpty(line)) continue;

                foreach (Type writerType in this._outputWriters)
                {
                    IWriter instance = (IWriter) Activator.CreateInstance(writerType);
                    instance.ProcessReadMessage(line, this);
                }

                ParsedMessage message = new ParsedMessage(this, line);

                foreach (RegisteredMessage messageType in this._registeredMessages)
                {
                    if (messageType.CheckMessage(message))
                    {
                        messageType.ProcessMessage(message);
                    }
                }
            }

            this.TriggerOnDisconnect();
        }

        /// <summary>
        /// Registers a message that will be checked when new messages are received.
        /// </summary>
        /// <param name="type">The type of the Message object that messages will be checked against.</param>
        public void RegisterMessage(Type type)
        {
            this._registeredMessages.Add(new RegisteredMessage(this, type));
        }

        private void RegisterMessages()
        {
            this.RegisterMessage(typeof(Messages.Receive.Ping));
            this.RegisterMessage(typeof(Messages.Receive.Join));
            this.RegisterMessage(typeof(Messages.Receive.Part));
            this.RegisterMessage(typeof(Messages.Receive.Nick));
            this.RegisterMessage(typeof(Messages.Receive.Topic));
            this.RegisterMessage(typeof(Messages.Receive.Quit));
            this.RegisterMessage(typeof(Messages.Receive.Kick));

            this.RegisterMessage(typeof(Messages.Receive.ChannelMode));
            this.RegisterMessage(typeof(Messages.Receive.ChannelPrivate));
            this.RegisterMessage(typeof(Messages.Receive.ChannelNotice));
            this.RegisterMessage(typeof(Messages.Receive.UserMode));
            this.RegisterMessage(typeof(Messages.Receive.UserPrivate));
            this.RegisterMessage(typeof(Messages.Receive.UserNotice));

            this.RegisterMessage(typeof(Messages.Receive.CTCP.Version));
            this.RegisterMessage(typeof(Messages.Receive.CTCP.VersionReply));

            this.RegisterMessage(typeof(Messages.Receive.CTCP.ChannelAction));
            this.RegisterMessage(typeof(Messages.Receive.CTCP.UserAction));

            this.RegisterMessage(typeof(Messages.Receive.Numerics.Away));
            this.RegisterMessage(typeof(Messages.Receive.Numerics.Welcome));
            this.RegisterMessage(typeof(Messages.Receive.Numerics.Names));
            this.RegisterMessage(typeof(Messages.Receive.Numerics.Who));
            this.RegisterMessage(typeof(Messages.Receive.Numerics.Topic));
            this.RegisterMessage(typeof(Messages.Receive.Numerics.TopicInfo));
            this.RegisterMessage(typeof(Messages.Receive.Numerics.NoTopic));
            this.RegisterMessage(typeof(Messages.Receive.Numerics.NowAway));
            this.RegisterMessage(typeof(Messages.Receive.Numerics.UnAway));
        }

        public void RegisterWriter(Type type)
        {
            if (!typeof(IWriter).IsAssignableFrom(type))
                throw new ArgumentException("type must implement IWriter", "type");

            this._outputWriters.Add(type);
        }

        private void RegisterWriters()
        {
            this.RegisterWriter(typeof(Output.ConsoleWriter));
            this.RegisterWriter(typeof(Output.IrcWriter));
        }

        /// <summary>
        /// Send a message to the connected server.
        /// </summary>
        /// <param name="message">The NetIRC.SendMessage instance to be sent.</param>
        public void Send(Messages.ISendMessage message)
        {
            SendMessageEventArgs e = new SendMessageEventArgs(message);
            this.TriggerOnSend(e);
            if (e.Message == null) return;

            using (MemoryStream stream = new MemoryStream())
            {
                message.Send(new StreamWriter(stream) {AutoFlush = true}, this);

                StreamReader reader = new StreamReader(stream);
                stream.Position = 0;

                while (true)
                {
                    string line = reader.ReadLine();

                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }

                    foreach (Type writerType in this._outputWriters)
                    {
                        IWriter instance = (IWriter)Activator.CreateInstance(writerType);
                        instance.ProcessSendMessage(line, this);
                    }
                }
            }
        }

        #region Events

        public delegate void OnChannelJoinHandler(Client client, Channel channel);
        public event OnChannelJoinHandler OnChannelJoin;

        internal void TriggerOnChannelJoin(Channel channel)
        {
            if (this.OnChannelJoin != null)
            {
                this.OnChannelJoin(this, channel);
            }
        }

        public delegate void OnChannelLeaveHandler(Client client, Channel channel);
        public event OnChannelLeaveHandler OnChannelLeave;

        internal void TriggerOnChannelLeave(Channel channel)
        {
            if (this.OnChannelLeave != null)
            {
                this.OnChannelLeave(this, channel);
            }
        }

        public delegate void OnConnectHandler(Client client);
        public event OnConnectHandler OnConnect;

        internal void TriggerOnConnect()
        {
            if (this.OnConnect != null)
            {
                this.OnConnect(this);
            }
        }

        public delegate void OnDisconnectHandler(Client client);
        public event OnDisconnectHandler OnDisconnect;

        private void TriggerOnDisconnect()
        {
            if (this.OnDisconnect != null)
            {
                this.OnDisconnect(this);
            }
        }

        public delegate void OnUserModeHandler(Client client, string modes);
        public event OnUserModeHandler OnUserMode;

        internal void TriggerOnUserMode(string modes)
        {
            if (this.OnUserMode != null)
            {
                this.OnUserMode(this, modes);
            }
        }

        public delegate void OnWelcomeHandler(Client client, string message);
        public event OnWelcomeHandler OnWelcome;

        internal void TriggerOnWelcome(string message)
        {
            if (this.OnWelcome != null)
            {
                this.OnWelcome(this, message);
            }
        }

        public delegate void OnWhoHandler(Client client, string message);
        public event OnWelcomeHandler OnWho;

        internal void TriggerOnWho(string message)
        {
            if (this.OnWho != null)
            {
                this.OnWho(this, message);
            }
        }

        public delegate void OnMessageHandler(Client client, User source, string message);
        public event OnMessageHandler OnMessage;

        public void TriggerOnMessage(User source, string message)
        {
            if (this.OnMessage != null)
            {
                this.OnMessage(this, source, message);
            }
        }

        public delegate void OnVersionHandler(Client client, User source);
        public event OnVersionHandler OnVersion;

        internal void TriggerOnVersion(User source)
        {
            if (this.OnVersion != null)
            {
                this.OnVersion(this, source);
            }
        }

        public delegate void OnVersionReplyHandler(Client client, User source, string version);
        public event OnVersionReplyHandler OnVersionReply;

        internal void TriggerOnVersionReply(User source, string version)
        {
            if (this.OnVersionReply != null)
            {
                this.OnVersionReply(this, source, version);
            }
        }

        public delegate void OnNoticeHandler(Client client, User source, string notice);
        public event OnNoticeHandler OnNotice;

        internal void TriggerOnNotice(User user, string notice)
        {
            if (this.OnNotice != null)
            {
                this.OnNotice(this, user, notice);
            }
        }

        public delegate void OnSendHandler(Client client, SendMessageEventArgs e);
        public event OnSendHandler OnSend;

        protected virtual void TriggerOnSend(SendMessageEventArgs e)
        {
            if (this.OnSend != null)
            {
                this.OnSend(this, e);
            }
        }

        public delegate void OnActionHandler(Client client, User source, string action);
        public event OnActionHandler OnAction;

        internal void TriggerOnAction(User user, string action)
        {
            if (this.OnAction != null)
            {
                this.OnAction(this, user, action);
            }
        }

        #endregion

        public void UnregisterMessage(Type type)
        {
            this._registeredMessages.RemoveAll(message => message.Type == type);
        }

        public void UnregisterWriter(Type type)
        {
            this._outputWriters.Remove(type);
        }
    }
}
