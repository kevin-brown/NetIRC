using System;
using System.Collections.Generic;

namespace NetIRC
{
    public class Channel
    {
        public string Name
        {
            get;
            private set;
        }

        public readonly ChannelTopic Topic = new ChannelTopic();

        public Dictionary<string, User> Users
        {
            get
            {
                return UserFactory.InChannel(this.Name);
            }
        }

        public readonly ChannelType Type;

        internal static Dictionary<ChannelType, char> TypeChars = new Dictionary<ChannelType, char>()
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

        internal void AddUser(User user)
        {
            if (this.Users.ContainsKey(user.NickName))
            {
                return;
            }

            user.Channels.Add(this);

            if (!user.Rank.ContainsKey(this.Name))
            {
                user.Rank.Add(this.Name, UserRank.None);
            }
        }

        public Messages.Send.TopicMessage GetTopic()
        {
            return new Messages.Send.TopicMessage(this);
        }

        public Messages.Send.InviteMessage Invite(User user)
        {
            return new Messages.Send.InviteMessage(this, user);
        }

        public Messages.Send.JoinMessage Join()
        {
            return new Messages.Send.JoinMessage(this);
        }

        internal void JoinUser(User user)
        {
            this.AddUser(user);

            this.TriggerOnJoin(user);
        }

        public Messages.Send.KickMessage Kick(User user)
        {
            return new Messages.Send.KickMessage(this, user);
        }

        public Messages.Send.KickMessage Kick(User user, string message)
        {
            return new Messages.Send.KickMessage(this, user, message);
        }

        public Messages.Send.PartMessage Part()
        {
            return new Messages.Send.PartMessage(this);
        }

        public Messages.Send.PartMessage Part(string message)
        {
            return new Messages.Send.PartMessage(this, message);
        }

        internal void RemoveUser(User user)
        {
            if (this.Users.ContainsKey(user.NickName))
            {
                user.Channels.Remove(this);

                this.TriggerOnLeave(user);
            }
        }

        public Messages.Send.NoticeMessage SendNotice(string message)
        {
		    return new Messages.Send.NoticeMessage(this, message);
        }

        public Messages.Send.ChatMessage SendMessage(string message)
        {
            return new Messages.Send.ChatMessage(this, message);
        }

        internal Messages.SendMessage SendWho()
        {
            return new Messages.Send.WhoMessage("#" + this.Name);
        }

        public Messages.Send.TopicMessage SetTopic(string topic)
        {
            return new Messages.Send.TopicMessage(this, topic);
        }

        public delegate void OnActionHandler(Channel source, User user, string action);
        public event OnActionHandler OnAction;

        internal void TriggerOnAction(User user, string action)
        {
            if (OnAction != null)
            {
                OnAction(this, user, action);
            }
        }

        public delegate void OnSendActionHandler(Channel source, string action);
        public event OnSendActionHandler OnSendAction;

        internal void TriggerOnSendAction(string action)
        {
            if (OnSendAction != null)
            {
                OnSendAction(this, action);
            }
        }

        public delegate void OnJoinHandler(Channel source, User user);
        public event OnJoinHandler OnJoin;

        internal void TriggerOnJoin(User user)
        {
            if (OnJoin != null)
            {
                OnJoin(this, user);
            }
        }

        public delegate void OnKickHandler(Channel source, User kicker, User user, string reason);
        public event OnKickHandler OnKick;

        internal void TriggerOnKick(User kicker, User user, string reason)
        {
            if (OnKick != null)
            {
                OnKick(this, kicker, user, reason);
            }
        }

        public delegate void OnLeaveHandler(Channel source, User user);
        public event OnLeaveHandler OnLeave;

        internal void TriggerOnLeave(User user)
        {
            if (OnLeave != null)
            {
                OnLeave(this, user);
            }
        }

        public delegate void OnMessageHandler(Channel source, User user, string message);
        public event OnMessageHandler OnMessage;

        internal void TriggerOnMessage(User user, string message)
        {
            if (OnMessage != null)
            {
                OnMessage(this, user, message);
            }
        }

        public delegate void OnSendMessageHandler(Channel source, string message);
        public event OnSendMessageHandler OnSendMessage;

        internal void TriggerOnSendMessage(string message)
        {
            if (OnSendMessage != null)
            {
                OnSendMessage(this, message);
            }
        }

        public delegate void OnNoticeHandler(Channel source, User user, string notice);
        public event OnNoticeHandler OnNotice;

        internal void TriggerOnNotice(User user, string notice)
        {
            if (OnNotice != null)
            {
                OnNotice(this, user, notice);
            }
        }

        public delegate void OnSendNoticeHandler(Channel source, string notice);
        public event OnSendNoticeHandler OnSendNotice;

        internal void TriggerOnSendNotice(string notice)
        {
            if (OnSendNotice != null)
            {
                OnSendNotice(this, notice);
            }
        }

        public delegate void OnTopicChangeHandler(Channel source, ChannelTopic topic);
        public event OnTopicChangeHandler OnTopicChange;

        internal void TriggerOnTopicChange(ChannelTopic topic)
        {
            if (OnTopicChange != null)
            {
                OnTopicChange(this, topic);
            }
        }

        public delegate void OnModeHandler(Channel source, User setter, string modes, string[] parameters);
        public event OnModeHandler OnMode;

        internal void TriggerOnMode(User setter, string modes, string[] parameters)
        {
            if (OnMode != null)
            {
                OnMode(this, setter, modes, parameters);
            }
        }

        public delegate void OnWhoHandler(Channel source, string message);
        public event OnWhoHandler OnWho;

        internal void TriggerOnWho(string message)
        {
            if (OnWho != null)
            {
                OnWho(this, message);
            }
        }
    }
}
