using System.Collections.Generic;

namespace NetIRC
{
    public class Channel
    {
        internal Client Client;

        /// <summary>
        /// Name of the channel without its prefix
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Full name of the channel with it's prefix (usually '#')
        /// </summary>
        public string FullName
        {
            get { return Channel.TypeChars[this.Type] + this.Name; } 
        }
        
        public readonly ChannelTopic Topic = new ChannelTopic();

        public ReadOnlyDictionary<string, User> Users
        {
            get
            {
                if (this.Client != null)
                {
                    return this.Client.UserFactory.InChannel(this.Name).AsReadOnly();
                }

                return new Dictionary<string, User>().AsReadOnly();
            }
        }

        public readonly ChannelType Type;

        internal static Dictionary<ChannelType, char> TypeChars = new Dictionary<ChannelType, char>
            {
                {ChannelType.Network, '#'},
                {ChannelType.Local, '&'},
                {ChannelType.Safe, '!'},
                {ChannelType.Unmoderated, '+'},
            };

        /// <summary>
        /// The channel is anonymous and all messages sent by users have an anonymous mask.
        /// </summary>
        public bool IsAnonymous
        {
            get;
            internal set;
        }

        /// <summary>
        /// The channel will only allow users in by invite.
        /// </summary>
        public bool IsInviteOnly
        {
            get;
            internal set;
        }

        /// <summary>
        /// The channel will only allow users with a rank of voice or above to speak.
        /// </summary>
        public bool IsModerated
        {
            get;
            internal set;
        }

        /// <summary>
        /// The channel will prevent anyone who is not in the channel from sending messages.
        /// </summary>
        public bool NoOutsideMessages
        {
            get;
            internal set;
        }

        /// <summary>
        /// The channel will not send information about user joins, parts or nick changes.
        /// </summary>
        public bool IsQuiet
        {
            get;
            internal set;
        }

        /// <summary>
        /// The channel is omitted from queries like WHOIS.
        /// </summary>
        public bool IsPrivate
        {
            get;
            internal set;
        }

        /// <summary>
        /// The channel is omitted from queries like TOPIC, LIST, and NAMES.
        /// </summary>
        public bool IsSecret
        {
            get;
            internal set;
        }

        /// <summary>
        /// The channel will reop some or all of its inhabitants after it has lost its channel operators for longer than the reop delay.
        /// </summary>
        public bool ServerReop
        {
            get;
            internal set;
        }

        /// <summary>
        /// The channel restricts the TOPIC command to operators.
        /// </summary>
        public bool IsTopicLocked
        {
            get;
            internal set;
        }

        /// <summary>
        /// The key that is required to join the channel.
        /// </summary>
        public string Key
        {
            get;
            internal set;
        }

        /// <summary>
        /// The limit on the amount of users that can join the channel. A UserLimit of -1 means that a limit is not set.
        /// </summary>
        public int UserLimit
        {
            get;
            internal set;
        }

        /// <summary>
        /// A list of masks that cannot be allowed in the channel.
        /// </summary>
        public readonly List<string> BanList = new List<string>();

        /// <summary>
        /// A list of masks that are allowed in the channel, even if they match the masks in the BanList.
        /// </summary>
        public readonly List<string> ExceptList = new List<string>();

        /// <summary>
        /// A list of masks that are allowed in the channel, even if the channel is invite-only.
        /// </summary>
        public readonly List<string> InviteList = new List<string>();

        public Channel(string name)
        {
            this.Name = name;
            this.Type = ChannelType.Network;
            this.UserLimit = -1;

            foreach (var pair in Channel.TypeChars)
            {
                if (pair.Value == name[0])
                {
                    this.Name = this.Name.Substring(1);
                    this.Type = pair.Key;
                    break;
                }
            }
        }

        public Channel(string name, ChannelType type)
        {
            this.Name = name;
            this.Type = type;
            this.UserLimit = -1;
        }

        internal void SetRank(User user, UserRank rank)
        {
            user._ranks[this.Name] = rank;
            this.TriggerOnRank(this, user, rank);
        }

        internal void AddUser(User user)
        {
            if (this.Users.ContainsKey(user.NickName))
            {
                return;
            }

            user._channels.Add(this.Name, this);

            this.TriggerOnUserAdded(user);

            if (!user._ranks.ContainsKey(this.Name))
            {
                user._ranks.Add(this.Name, UserRank.None);
            }
        }

        public Messages.Send.Topic GetTopic()
        {
            return new Messages.Send.Topic(this);
        }

        public Messages.Send.Invite Invite(User user)
        {
            return new Messages.Send.Invite(this, user);
        }

        public Messages.Send.Join Join()
        {
            return new Messages.Send.Join(this);
        }

        internal void JoinUser(User user)
        {
            this.AddUser(user);

            this.TriggerOnJoin(user);
        }

        public Messages.Send.Kick Kick(User user)
        {
            return new Messages.Send.Kick(this, user);
        }

        public Messages.Send.Kick Kick(User user, string message)
        {
            return new Messages.Send.Kick(this, user, message);
        }

        public Messages.Send.Part Part()
        {
            return new Messages.Send.Part(this);
        }

        public Messages.Send.Part Part(string message)
        {
            return new Messages.Send.Part(this, message);
        }

        internal void RemoveUser(User user)
        {
            if (this.Users.ContainsKey(user.NickName))
            {
                user._channels.Remove(this.Name);
                user._ranks.Remove(this.Name);
            }
        }

        internal void LeaveUser(User user, string reason)
        {
            this.RemoveUser(user);
            this.TriggerOnLeave(user, reason);
        }

        internal void ClearUsers()
        {
            foreach (User user in this.Users.Values)
            {
                this.RemoveUser(user);
            }
        }

        public Messages.Send.ChannelNotice SendNotice(string message)
        {
            return new Messages.Send.ChannelNotice(this, message);
        }

        public Messages.Send.ChannelPrivate SendMessage(string message)
        {
            return new Messages.Send.ChannelPrivate(this, message);
        }

        internal Messages.ISendMessage SendWho()
        {
            return new Messages.Send.Who(this.FullName);
        }

        public Messages.Send.Topic SetTopic(string topic)
        {
            return new Messages.Send.Topic(this, topic);
        }

        public delegate void OnActionHandler(Channel source, User user, string action);
        public event OnActionHandler OnAction;

        internal void TriggerOnAction(User user, string action)
        {
            if (this.OnAction != null)
            {
                this.OnAction(this, user, action);
            }
        }

        public delegate void OnSendActionHandler(Channel source, string action);
        public event OnSendActionHandler OnSendAction;

        internal void TriggerOnSendAction(string action)
        {
            if (this.OnSendAction != null)
            {
                this.OnSendAction(this, action);
            }
        }

        public delegate void OnJoinHandler(Channel source, User user);
        public event OnJoinHandler OnJoin;

        internal void TriggerOnJoin(User user)
        {
            if (this.OnJoin != null)
            {
                this.OnJoin(this, user);
            }
        }

        public delegate void OnKickHandler(Channel source, User kicker, User user, string reason);
        public event OnKickHandler OnKick;

        internal void TriggerOnKick(User kicker, User user, string reason)
        {
            if (this.OnKick != null)
            {
                this.OnKick(this, kicker, user, reason);
            }
        }

        public delegate void OnLeaveHandler(Channel source, User user, string reason);
        public event OnLeaveHandler OnLeave;

        internal void TriggerOnLeave(User user, string reason)
        {
            if (this.OnLeave != null)
            {
                this.OnLeave(this, user, reason);
            }
        }

        public delegate void OnMessageHandler(Channel source, User user, string message);
        public event OnMessageHandler OnMessage;

        internal void TriggerOnMessage(User user, string message)
        {
            if (this.OnMessage != null)
            {
                this.OnMessage(this, user, message);
            }
        }

        public delegate void OnSendMessageHandler(Channel source, string message);
        public event OnSendMessageHandler OnSendMessage;

        internal void TriggerOnSendMessage(string message)
        {
            if (this.OnSendMessage != null)
            {
                this.OnSendMessage(this, message);
            }
        }

        public delegate void OnNoticeHandler(Channel source, User user, string notice);
        public event OnNoticeHandler OnNotice;

        internal void TriggerOnNotice(User user, string notice)
        {
            if (this.OnNotice != null)
            {
                this.OnNotice(this, user, notice);
            }
        }

        public delegate void OnSendNoticeHandler(Channel source, string notice);
        public event OnSendNoticeHandler OnSendNotice;

        internal void TriggerOnSendNotice(string notice)
        {
            if (this.OnSendNotice != null)
            {
                this.OnSendNotice(this, notice);
            }
        }

        public delegate void OnTopicChangeHandler(Channel source, ChannelTopic topic);
        public event OnTopicChangeHandler OnTopicChange;

        internal void TriggerOnTopicChange(ChannelTopic topic)
        {
            if (this.OnTopicChange != null)
            {
                this.OnTopicChange(this, topic);
            }
        }

        public delegate void OnModeHandler(Channel source, User setter, string modes, string[] parameters);
        public event OnModeHandler OnMode;

        internal void TriggerOnMode(User setter, string modes, string[] parameters)
        {
            if (this.OnMode != null)
            {
                this.OnMode(this, setter, modes, parameters);
            }
        }

        public delegate void OnWhoHandler(Channel source, User user, string message);
        public event OnWhoHandler OnWho;

        internal void TriggerOnWho(User user, string message)
        {
            if (this.OnWho != null)
            {
                this.OnWho(this, user, message);
            }
        }

        public delegate void OnUserAddedHandler(Channel source, User user);
        public event OnUserAddedHandler OnUserAdded;

        internal void TriggerOnUserAdded(User user)
        {
            if (this.OnUserAdded != null)
            {
                this.OnUserAdded(this, user);
            }
        }

        public delegate void OnNamesHandler(Channel source, string[] users);
        public event OnNamesHandler OnNames;

        internal void TriggerOnNames(string[] users)
        {
            if (this.OnNames != null)
            {
                this.OnNames(this, users);
            }
        }

        public delegate void OnRankHandler(Channel source, User user, UserRank rank);
        public event OnRankHandler OnRank;

        internal virtual void TriggerOnRank(Channel source, User user, UserRank rank)
        {
            OnRankHandler handler = this.OnRank;
            if (handler != null) handler(source, user, rank);
        }
    }
}
