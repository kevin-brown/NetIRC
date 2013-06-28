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
                parts[2].StartsWith("#") == true; //it is a channel
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            User setter = ReceiveUserMessage.GetUser(message);
            Channel channel = client.Channels[parts[2].Remove(0, 1)];
            
            string modes = parts[3];

            //check if it's a chanusermode or just a chanmode based on the flag and # of parameters
            if (parts.Length > 4)
            {
                User target = UserFactory.FromNick(parts[4]);

                if (channel.Users.ContainsValue(target))
                {
                    channel.TriggerOnUserMode(setter, target, modes);
                }

                else
                {
                    channel.TriggerOnMode(setter, modes, parts.Skip(4).ToArray());
                }
            }

            else
            {
                ParseModes(channel, modes);

                channel.TriggerOnMode(setter, modes, null);
            }
        }

        public void ParseModes(Channel target, string modes)
        {
            bool addMode = true;
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
                            target.InviteOnly = true;
                        else
                            target.InviteOnly = false;
                        break;
                    case 'm':
                        if (addMode)
                            target.Moderated = true;
                        else
                            target.Moderated = false;
                        break;
                    case 'n':
                        if (addMode)
                            target.NoOutsideMessages = true;
                        else
                            target.NoOutsideMessages = false;
                        break;
                    case 'p':
                        if (addMode)
                            target.Private = true;
                        else
                            target.Private = false;
                        break;
                    case 's':
                        if (addMode)
                            target.Secret = true;
                        else
                            target.Secret = false;
                        break;
                    case 't':
                        if (addMode)
                            target.TopicLock = true;
                        else
                            target.TopicLock = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
