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

        public Channel(string name)
        {
            this.Name = name;
        }

        internal void AddUser(User user)
        {
            if (this.Users.ContainsKey(user.NickName))
            {
                return;
            }

            this.TriggerOnJoin(user);

            user.Channels.Add(this);
        }

        internal void RemoveUser(User user)
        {
            if (this.Users.ContainsKey(user.NickName))
            {
                this.TriggerOnLeave(user);

                user.Channels.Remove(this);
            }
        }

        internal Messages.SendMessage SendWho()
        {
            return new Messages.Send.WhoMessage("#" + this.Name);
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

        public delegate void OnTopicChangeHandler(Channel source, ChannelTopic topic);
        public event OnTopicChangeHandler OnTopicChange;

        internal void TriggerOnTopicChange(ChannelTopic topic)
        {
            if (OnTopicChange != null)
            {
                OnTopicChange(this, topic);
            }
        }
    }
}
