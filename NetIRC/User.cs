﻿using System;
using System.Collections.Generic;

namespace NetIRC
{
    public class User
    {
        /// <summary>
        /// The nickname that the user will be associated with.
        /// </summary>
        public string NickName
        {
            get;
            internal set;
        }

        private string _userName = string.Empty;

        /// <summary>
        /// The username (sometimes called ident) that is associated with the user.
        /// </summary>
        public string UserName
        {
            get
            {
                return this._userName;
            }

            internal set
            {
                if (this._userName == value)
                {
                    return;
                }

                string original = this._userName;

                this._userName = value;

                this.TriggerOnUserNameChange(original);
            }
        }

        private string _hostName = string.Empty;

        /// <summary>
        /// The hostname that is attached to the user.
        /// </summary>
        public string HostName
        {
            get
            {
                return this._hostName;
            }

            internal set
            {
                if (this._hostName == value)
                {
                    return;
                }

                string original = this._hostName;

                this._hostName = value;

                this.TriggerOnHostNameChange(original);
            }
        }

        /// <summary>
        /// The real name that the user specified when connecting.
        /// </summary>
        public string RealName
        {
            get;
            internal set;
        }

        private bool _isAway;

        /// <summary>
        /// The user is away.
        /// </summary>
        public bool IsAway
        {
            get { return this._isAway; }
            internal set
            {
                if (this._isAway == value) return;
                this._isAway = value;
                this.TriggerOnIsAwayChange();
            }
        }

        private string _awayMessage;

        /// <summary>
        /// The current away message of the user.
        /// </summary>
        public string AwayMessage
        {
            get { return this._awayMessage; }
            internal set
            {
                if (this._awayMessage == value) return;

                string original = this._hostName;

                this._awayMessage = value;
                this.TriggerOnAwayMessageChange(original);
            }
        }

        /// <summary>
        /// The user is marked as invisible.
        /// </summary>
        public bool IsInvisible
        {
            get;
            internal set;
        }

        /// <summary>
        /// The user is receiving WallOps.
        /// </summary>
        public bool IsReceivingWallOps
        {
            get;
            internal set;
        }

        /// <summary>
        /// The user is restricted.
        /// </summary>
        public bool IsRestricted
        {
            get;
            internal set;
        }

        /// <summary>
        /// Whether or not the user is an IRC operator.
        /// </summary>
        public bool IsOperator
        {
            get;
            internal set;
        }

        /// <summary>
        /// The user can receive server notices. (Obsolete in RFC2812)
        /// </summary>
        public bool IsReceivingServerNotices
        {
            get;
            internal set;
        }

        internal readonly Dictionary<string, Channel> _channels = new Dictionary<string, Channel>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// The list of channels that the user is in.
        /// </summary>
        public ReadOnlyDictionary<string, Channel> Channels
        {
            get
            {
                return this._channels.AsReadOnly();
            }
        }

        internal readonly Dictionary<string, UserRank> _ranks = new Dictionary<string, UserRank>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// A dictionary that contains the current ranks for the user in each of their channels.
        /// </summary>
        public ReadOnlyDictionary<string, UserRank> Ranks
        {
            get 
            { 
                return this._ranks.AsReadOnly();
            }
        }

        /// <summary>
        /// A dictionary that represents the characters associated with specific ranks in WHO and NAMES messages.
        /// </summary>
        internal static Dictionary<char, UserRank> RankChars = new Dictionary<char, UserRank>
            {
                {'+', UserRank.Voice},
                {'%', UserRank.HalfOp},
                {'@', UserRank.Op},
                {'&', UserRank.Admin},
                {'~', UserRank.Owner},
            };

        public User()
        {
            
        }

        public User(string nick)
        {
            this.NickName = nick;
        }

        public User(string nick, string user)
        {
            this.NickName = nick;
            this.UserName = user;
        }

        public User(string nick, string user, string real)
        {
            this.NickName = nick;
            this.RealName = real;
            this.UserName = user;
        }

        public Messages.Send.Invite Invite(Channel channel)
        {
            return channel.Invite(this);
        }

        public delegate void OnHostNameChangeHandler(User user, string original);
        public event OnHostNameChangeHandler OnHostNameChange;

        internal void TriggerOnHostNameChange(string original)
        {
            if (this.OnHostNameChange != null)
            {
                this.OnHostNameChange(this, original);
            }
        }

        public delegate void OnNickNameChangeHandler(User user, string original);
        public event OnNickNameChangeHandler OnNickNameChange;

        internal void TriggerOnNickNameChange(string original)
        {
            if (this.OnNickNameChange != null)
            {
                this.OnNickNameChange(this, original);
            }
        }

        public delegate void OnQuitHandler(User user, string reason);
        public event OnQuitHandler OnQuit;

        internal void TriggerOnQuit(string reason)
        {
            if (this.OnQuit != null)
            {
                this.OnQuit(this, reason);
            }
        }

        public delegate void OnUserNameChangeHandler(User user, string original);
        public event OnUserNameChangeHandler OnUserNameChange;

        internal void TriggerOnUserNameChange(string original)
        {
            if (this.OnUserNameChange != null)
            {
                this.OnUserNameChange(this, original);
            }
        }

        public delegate void OnIsAwayChangeHandler(User user);
        public event OnIsAwayChangeHandler OnIsAwayChange;

        internal void TriggerOnIsAwayChange()
        {
            if (this.OnIsAwayChange != null)
            {
                this.OnIsAwayChange(this);
            }
        }

        public delegate void OnAwayMessageChangeHandler(User user, string original);
        public event OnAwayMessageChangeHandler OnAwayMessageChange;

        internal void TriggerOnAwayMessageChange(string original)
        {
            if (this.OnAwayMessageChange != null)
            {
                this.OnAwayMessageChange(this, original);
            }
        }
    }
}
