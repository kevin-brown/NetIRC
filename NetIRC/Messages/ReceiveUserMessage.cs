using System;
using System.Text.RegularExpressions;

namespace NetIRC.Messages
{
    abstract class ReceiveUserMessage : ReceiveMessage
    {
        public static bool CheckCommand(string message, string command)
        {
            if (!message.StartsWith(":"))
            {
                return false;
            }

            string[] parts = message.Split(' ');

            if (parts.Length < 2)
            {
                return false;
            }

            if (parts[1].ToLower() != command.ToLower())
            {
                return false;
            }

            return true;
        }

        public User GetUser(string message)
        {
            string[] parts = message.Split(' ');
            string userMask = parts[0].Substring(1);

            Match matches = Regex.Match(userMask, @"^([A-Za-z0-9\-]+)!([A-Za-z0-9\-\~]+)\@([A-Za-z0-9\.\-]+)", RegexOptions.IgnoreCase);

            if (!matches.Success)
            {
                return null;
            }

            return new User(matches.Groups[1].Value, matches.Groups[3].Value, matches.Groups[2].Value);
        }

        abstract public void ProcessMessage(string message, Client client);
    }
}
