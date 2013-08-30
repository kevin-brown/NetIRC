﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetIRC.Messages.Receive
{
    class UserNoticeMessage : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "NOTICE" &&
                   !message.IsChannel() &&
                   !message.IsCTCP();
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User user = message.GetUser();
            string notice = message.Parameters[1];

            client.TriggerOnNotice(user, notice);
        }
    }
}
