using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class ChannelModeMessage : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "MODE" &&
                   message.IsChannel();
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User setter = message.GetUser();
            Channel channel = message.GetChannel();
            string modes = message.Parameters[1];
            string[] parameters = message.Parameters[2].Split(' ');

            ParseModes(client, channel, modes, parameters);
            channel.TriggerOnMode(setter, modes, parameters);
        }

        public void ParseModes(Client client, Channel target, string modes, string[] parameters)
        {
            bool addMode = true;
            int paramIndex = 0;
            foreach (char mode in modes)
            {
                User userTarget;
                string param;

                switch (mode)
                {
                    case '+': 
                        addMode = true;
                        break;
                    case '-': 
                        addMode = false;
                        break;
                    case 'i':
                        target.IsInviteOnly = addMode;
                        break;
                    case 'm':
                        target.IsModerated = addMode;
                        break;
                    case 'n':
                        target.NoOutsideMessages = addMode;
                        break;
                    case 'p':
                        target.IsPrivate = addMode;
                        break;
                    case 's':
                        target.IsSecret = addMode;
                        break;
                    case 't':
                        target.IsTopicLocked = addMode;
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
                }
            }
        }
    }
}
