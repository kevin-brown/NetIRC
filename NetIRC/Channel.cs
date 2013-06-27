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

        public Channel(string name)
        {
            this.Name = name;
            this.Type = ChannelType.Network;

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

        public delegate void OnUserModeHandler(Channel source, User setter, User target, string modes);
        public event OnUserModeHandler OnUserMode;

        internal void TriggerOnUserMode(User setter, User target, string modes)
        {
            if (OnUserMode != null)
            {
                OnUserMode(this, setter, target, modes);
            }
        }
    }
}
