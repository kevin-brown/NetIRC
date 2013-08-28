using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class ChannelModeMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            return ReceiveUserMessage.CheckCommand(message, "MODE") &&
                Channel.TypeChars.Values.Contains(parts[2][0]) == true; //it is a channel
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            User setter = ReceiveUserMessage.GetUser(client, message);
            Channel channel = client.Channels[parts[2].Remove(0, 1)];

            string modes = parts[3];

            ParseModes(client, channel, modes, parts.Skip(4).ToArray());

            channel.TriggerOnMode(setter, modes, parts.Skip(4).ToArray());
        }

        public void ParseModes(Client client, Channel target, string modes, string[] parameters)
        {
            bool addMode = true;
            int paramIndex = 0;
            User userTarget = null;
            string param = null;
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
                            userTarget = client.UserFactory.FromNick(parameters[paramIndex]);
                            if (target.Users.ContainsValue(userTarget))
                                if (addMode)
                                    userTarget._ranks[target.Name] |= UserRank.Voice;
                                else
                                    userTarget._ranks[target.Name] &= ~UserRank.Voice;
                            paramIndex++;
                        }
                        break;
                    case 'h':
                        if (paramIndex < parameters.Length)
                        {
                            userTarget = client.UserFactory.FromNick(parameters[paramIndex]);
                            if (target.Users.ContainsValue(userTarget))
                                if (addMode)
                                    userTarget._ranks[target.Name] |= UserRank.HalfOp;
                                else
                                    userTarget._ranks[target.Name] &= ~UserRank.HalfOp;
                            paramIndex++;
                        }
                        break;
                    case 'o':
                        if (paramIndex < parameters.Length)
                        {
                            userTarget = client.UserFactory.FromNick(parameters[paramIndex]);
                            if (target.Users.ContainsValue(userTarget))
                                if (addMode)
                                    userTarget._ranks[target.Name] |= UserRank.Op;
                                else
                                    userTarget._ranks[target.Name] &= ~UserRank.Op;
                            paramIndex++;
                        }
                        break;
                    case 'a':
                        if (paramIndex < parameters.Length)
                        {
                            userTarget = client.UserFactory.FromNick(parameters[paramIndex]);
                            if (target.Users.ContainsValue(userTarget))
                                if (addMode)
                                    userTarget._ranks[target.Name] |= UserRank.Admin;
                                else
                                    userTarget._ranks[target.Name] &= ~UserRank.Admin;
                            paramIndex++;
                        }
                        break;
                    case 'q':
                        if (paramIndex < parameters.Length)
                        {
                            userTarget = client.UserFactory.FromNick(parameters[paramIndex]);
                            if (target.Users.ContainsValue(userTarget))
                                if (addMode)
                                    userTarget._ranks[target.Name] |= UserRank.Owner;
                                else
                                    userTarget._ranks[target.Name] &= ~UserRank.Owner;
                            paramIndex++;
                        }
                        break;
                    case 'b':
                        if (paramIndex < parameters.Length)
                        {
                            param = parameters[paramIndex];
                            if (addMode)
                                target.BanList.Add(param);
                            else if (target.BanList.Contains(param))
                                target.BanList.Remove(param);
                            paramIndex++;
                        }
                        break;
                    case 'e':
                        if (paramIndex < parameters.Length)
                        {
                            param = parameters[paramIndex];
                            if (addMode)
                                target.ExceptList.Add(param);
                            else if (target.ExceptList.Contains(param))
                                target.ExceptList.Remove(param);
                            paramIndex++;
                        }
                        break;
                    case 'I':
                        if (paramIndex < parameters.Length)
                        {
                            param = parameters[paramIndex];
                            if (addMode)
                                target.InviteList.Add(param);
                            else if (target.InviteList.Contains(param))
                                target.InviteList.Remove(param);
                            paramIndex++;
                        }
                        break;
                    case 'k':
                        if (paramIndex < parameters.Length) //MODE +spk test, MODE +skp test, MODE -k
                        {
                            param = parameters[paramIndex];
                            if (addMode)
                                target.Key = param;
                            else if (param == target.Key)
                                target.Key = null;
                            paramIndex++;
                        }
                        break;
                    case 'l':
                        if (paramIndex < parameters.Length && addMode)
                        {
                            param = parameters[paramIndex];
                            int limit;
                            if (Int32.TryParse(param, out limit))
                                target.UserLimit = limit;
                            paramIndex++;
                        }

                        else if (addMode == false)
                        {
                            target.UserLimit = -1;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
