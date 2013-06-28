using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class ChannelModeMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            string[] parts = message.Split(' ');

            return ReceiveUserMessage.CheckCommand(message, "MODE") &&
                Channel.TypeChars.Values.Contains(parts[2][0]) == true; //it is a channel
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            User setter = ReceiveUserMessage.GetUser(message);
            Channel channel = client.Channels[parts[2].Remove(0, 1)];

            string modes = parts[3];

            ParseModes(channel, modes, parts.Skip(4).ToArray());

            channel.TriggerOnMode(setter, modes, parts.Skip(4).ToArray());
        }

        public void ParseModes(Channel target, string modes, string[] parameters)
        {
            bool addMode = true;
            int paramIndex = 0;
            User userTarget = null;
            string mask = null;
            for (int i = 0; i < modes.Length; i++)
            {
                switch (modes[i])
                {
                    case '+': addMode = true;
                        break;
                    case '-': addMode = false;
                        break;
                    case 'i':
                        if (addMode)
                            target.IsInviteOnly = true;
                        else
                            target.IsInviteOnly = false;
                        break;
                    case 'm':
                        if (addMode)
                            target.IsModerated = true;
                        else
                            target.IsModerated = false;
                        break;
                    case 'n':
                        if (addMode)
                            target.NoOutsideMessages = true;
                        else
                            target.NoOutsideMessages = false;
                        break;
                    case 'p':
                        if (addMode)
                            target.IsPrivate = true;
                        else
                            target.IsPrivate = false;
                        break;
                    case 's':
                        if (addMode)
                            target.IsSecret = true;
                        else
                            target.IsSecret = false;
                        break;
                    case 't':
                        if (addMode)
                            target.IsTopicLocked = true;
                        else
                            target.IsTopicLocked = false;
                        break;
                    case 'v':
                        if (paramIndex < parameters.Length)
                        {
                            userTarget = UserFactory.FromNick(parameters[paramIndex]);
                            if (target.Users.ContainsValue(userTarget))
                                if (addMode && userTarget.Rank[target.Name] < UserRank.Voice)
                                    userTarget.Rank[target.Name] = UserRank.Voice;
                            paramIndex++;
                        }
                        break;
                    case 'h':
                        if (paramIndex < parameters.Length)
                        {
                            userTarget = UserFactory.FromNick(parameters[paramIndex]);
                            if (target.Users.ContainsValue(userTarget))
                                if (addMode && userTarget.Rank[target.Name] < UserRank.HalfOp)
                                    userTarget.Rank[target.Name] = UserRank.HalfOp;
                            paramIndex++;
                        }
                        break;
                    case 'o':
                        if (paramIndex < parameters.Length)
                        {
                            userTarget = UserFactory.FromNick(parameters[paramIndex]);
                            if (target.Users.ContainsValue(userTarget))
                                if (addMode && userTarget.Rank[target.Name] < UserRank.Op)
                                    userTarget.Rank[target.Name] = UserRank.Op;
                            paramIndex++;
                        }
                        break;
                    case 'a':
                        if (paramIndex < parameters.Length)
                        {
                            userTarget = UserFactory.FromNick(parameters[paramIndex]);
                            if (target.Users.ContainsValue(userTarget))
                                if (addMode && userTarget.Rank[target.Name] < UserRank.Admin)
                                    userTarget.Rank[target.Name] = UserRank.Admin;
                            paramIndex++;
                        }
                        break;
                    case 'q':
                        if (paramIndex < parameters.Length)
                        {
                            userTarget = UserFactory.FromNick(parameters[paramIndex]);
                            if (target.Users.ContainsValue(userTarget))
                                if (addMode && userTarget.Rank[target.Name] < UserRank.Owner)
                                    userTarget.Rank[target.Name] = UserRank.Owner;
                            paramIndex++;
                        }
                        break;
                    case 'b':
                        if (paramIndex < parameters.Length)
                        {
                            mask = parameters[paramIndex];
                            if (addMode)
                                target.BanList.Add(mask);
                            else if (target.BanList.Contains(mask))
                                target.BanList.Remove(mask);
                            paramIndex++;
                        }
                        break;
                    case 'e':
                        if (paramIndex < parameters.Length)
                        {
                            mask = parameters[paramIndex];
                            if (addMode)
                                target.ExceptList.Add(mask);
                            else if (target.ExceptList.Contains(mask))
                                target.ExceptList.Remove(mask);
                            paramIndex++;
                        }
                        break;
                    case 'I':
                        if (paramIndex < parameters.Length)
                        {
                            mask = parameters[paramIndex];
                            if (addMode)
                                target.InviteList.Add(mask);
                            else if (target.InviteList.Contains(mask))
                                target.InviteList.Remove(mask);
                            paramIndex++;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
