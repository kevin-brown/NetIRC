using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NetIRC
{
    public class ParsedMessage
    {
        private const string ctcpChar = "\x001";

        private readonly Client _client;

        public string Prefix { get; private set; }

        public string Command { get; private set; }

        public string[] Parameters { get; private set; }

        public int Length
        {
            get { return Parameters.Length; }
        }

        public string Message { get; private set; }

        public ParsedMessage(Client client, string message)
        {
            this._client = client;
            this.Message = message;

            Regex parsingRegex =
                new Regex(@"^(:(?<prefix>\S+) )?(?<command>\S+)( (?!:)(?<params>.+?))?( :(?<trail>.+))?$",
                          RegexOptions.ExplicitCapture);
            Match messageMatch = parsingRegex.Match(message);

            if (messageMatch.Success)
            {
                Prefix = messageMatch.Groups["prefix"].Value;
                Command = messageMatch.Groups["command"].Value;
                Parameters = messageMatch.Groups["params"].Value.Split(' ');
                string trail = messageMatch.Groups["trail"].Value;

                if (!String.IsNullOrEmpty(trail))
                    Parameters = Parameters.Concat(new[] {trail}).ToArray();
            }
        }

        /// <summary>
        /// Gets or generates a User object from the UserFactory
        /// </summary>
        public User GetUser(string userMask)
        {
            return _client.UserFactory.FromUserMask(userMask);
        }

        /// <summary>
        /// Gets or generates a Channel object from the ChannelFactory
        /// </summary>
        public Channel GetChannel(string channel)
        {
            if (!Channel.TypeChars.Values.Contains(channel[0]))
                return null;

            return _client.ChannelFactory.FromName(channel.Substring(1));
        }

        /// <summary>
        /// Gets the User who sent the message or null
        /// </summary>
        public User GetUser()
        {
            return GetUser(Prefix);
        }

        /// <summary>
        /// Gets the Channel the message is targeted at or null
        /// </summary>
        public Channel GetChannel()
        {
            return GetChannel(Parameters[0]);
        }

        /// <summary>
        /// Determines whether the given userMask is valid
        /// </summary>
        public bool IsUser(string userMask)
        {
            return GetUser(userMask) != null;
        }

        /// <summary>
        /// Determines whether the given channel is valid
        /// </summary>
        public bool IsChannel(string channel)
        {
            return GetChannel(channel) != null;
        }

        /// <summary>
        /// Determines whether the message is originated from a user
        /// </summary>
        public bool IsUser()
        {
            return GetUser() != null;
        }

        /// <summary>
        /// Determines whether the message is targeted at a channel
        /// </summary>
        public bool IsChannel()
        {
            return GetChannel() != null;
        }

        /// <summary>
        /// Checks if the message is a numeric one
        /// </summary>
        public bool IsNumeric()
        {
            return Command.Length == 3 && Command.All(char.IsDigit);
        }

        /// <summary>
        /// Gets the CTCP message or null
        /// </summary>
        public string GetCTCP(string message)
        {
            if (!message.StartsWith(ctcpChar) || !message.EndsWith(ctcpChar))
            {
                return null;
            }

            return message.Substring(1, message.Length - 2);
        }

        /// <summary>
        /// Gets the CTCP command
        /// </summary>
        public string GetCTCPCommand(string message)
        {
            return GetCTCP(message).Split(' ')[0];
        }

        /// <summary>
        /// Gets the CTCP arguments 
        /// </summary>
        public string[] GetCTCPArguments(string message)
        {
            return GetCTCP(message).Split(' ').Skip(1).ToArray();
        }

        /// <summary>
        /// Checks if the message is a Client-to-client protocol message
        /// </summary>
        public bool IsCTCP(string message)
        {
            return GetCTCP(message) != null;
        }
    }
}
