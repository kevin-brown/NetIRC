using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetIRC.Messages
{
    public class ParsedMessage
    {
        private const string CTCPChar = "\x001";

        private readonly Client _client;

        public string Prefix
        {
            get; 
            private set;
        }

        public string Command
        {
            get; 
            private set;
        }

        public string[] Parameters
        {
            get; 
            private set;
        }

        public int Length
        {
            get
            {
                return this.Parameters.Length;
            }
        }

        public string Message
        {
            get; 
            private set;
        }

        public ParsedMessage(Client client, string message)
        {
            this._client = client;
            this.Message = message;
            this.Parameters = new string[0];

            Regex parsingRegex =
                new Regex(@"^(:(?<prefix>\S+) )?(?<command>\S+)( (?!:)(?<params>.+?))?( :(?<trail>.+))?$",
                          RegexOptions.ExplicitCapture);
            Match messageMatch = parsingRegex.Match(message);

            if (messageMatch.Success)
            {
                this.Prefix = messageMatch.Groups["prefix"].Value;
                this.Command = messageMatch.Groups["command"].Value;

                string paramsStr = messageMatch.Groups["params"].Value;
                if (!String.IsNullOrEmpty(paramsStr))
                    this.Parameters = paramsStr.Split(' ');

                string trail = messageMatch.Groups["trail"].Value;
                if (!String.IsNullOrEmpty(trail))
                    this.Parameters = this.Parameters.Concat(new[] {trail}).ToArray();
            }
        }

        /// <summary>
        /// Gets or generates a User object from the UserFactory
        /// </summary>
        public User GetUser(string userMask)
        {
            return this._client.UserFactory.FromUserMask(userMask);
        }

        /// <summary>
        /// Gets or generates a User object from the UserFactory
        /// </summary>
        public User GetUserFromNick(string nick)
        {
            return this._client.UserFactory.FromNick(nick);
        }

        /// <summary>
        /// Gets or generates a Channel object from the ChannelFactory
        /// </summary>
        public Channel GetChannel(string channel)
        {
            if (!Channel.TypeChars.Values.Contains(channel[0]))
                return null;

            return this._client.ChannelFactory.FromName(channel.Substring(1));
        }

        /// <summary>
        /// Gets the User who sent the message or null
        /// </summary>
        public User GetUser()
        {
            return this.GetUser(this.Prefix);
        }

        /// <summary>
        /// Gets the Channel the message is targeted at or null
        /// </summary>
        public Channel GetChannel()
        {
            return this.GetChannel(this.Parameters[0]);
        }

        /// <summary>
        /// Determines whether the given userMask is valid
        /// </summary>
        public bool IsUser(string userMask)
        {
            return this.GetUser(userMask) != null;
        }

        /// <summary>
        /// Determines whether the given channel is valid
        /// </summary>
        public bool IsChannel(string channel)
        {
            return this.GetChannel(channel) != null;
        }

        /// <summary>
        /// Determines whether the message is originated from a user
        /// </summary>
        public bool IsUser()
        {
            return this.IsUser(this.Parameters[0]);
        }

        /// <summary>
        /// Determines whether the message is targeted at a channel
        /// </summary>
        public bool IsChannel()
        {
            return this.IsChannel(this.Parameters[0]);
        }

        /// <summary>
        /// Checks if the message is a numeric one
        /// </summary>
        public bool IsNumeric()
        {
            return this.Command.Length == 3 && this.Command.All(char.IsDigit);
        }

        /// <summary>
        /// Gets the CTCP message or null
        /// </summary>
        public static string GetCTCP(string message)
        {
            if (!message.StartsWith(CTCPChar) || !message.EndsWith(CTCPChar))
            {
                return null;
            }

            return message.Substring(1, message.Length - 2);
        }

        /// <summary>
        /// Gets the CTCP message or null
        /// </summary>
        public string GetCTCP()
        {
            return GetCTCP(this.Parameters[1]);
        }

        /// <summary>
        /// Gets the CTCP command
        /// </summary>
        public static string GetCTCPCommand(string message)
        {
            return GetCTCP(message).Split(' ')[0];
        }

        /// <summary>
        /// Gets the CTCP command
        /// </summary>
        public string GetCTCPCommand()
        {
            return GetCTCPCommand(this.Parameters[1]);
        }

        /// <summary>
        /// Gets the CTCP arguments 
        /// </summary>
        public static string GetCTCPParameter(string message)
        {
            string ctcp = GetCTCP(message);
            if (ctcp.Contains(' '))
                return string.Join(" ", ctcp.Split(' ').Skip(1));

            return null;
        }

        /// <summary>
        /// Gets the CTCP arguments 
        /// </summary>
        public string GetCTCPParameter()
        {
            return GetCTCPParameter(this.Parameters[1]);
        }

        /// <summary>
        /// Checks if the ctcp message contains parameters
        /// </summary>
        public static bool HasCTCPParameter(string message)
        {
            return GetCTCPParameter(message) != null;
        }

        /// <summary>
        /// Checks if the ctcp message contains parameters
        /// </summary>
        public  bool HasCTCPParameter()
        {
            return HasCTCPParameter(this.Parameters[1]);
        }

        /// <summary>
        /// Checks if the message is a Client-to-client protocol message
        /// </summary>
        public static bool IsCTCP(string message)
        {
            return GetCTCP(message) != null;
        }

        /// <summary>
        /// Checks if the message is a Client-to-client protocol message
        /// </summary>
        public bool IsCTCP()
        {
            return IsCTCP(this.Parameters[1]);
        }
    }
}
