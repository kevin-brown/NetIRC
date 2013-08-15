﻿using System;
using System.IO;

namespace NetIRC.Tests
{
    class TestHelpers
    {
        public static string GetSendMessageOutput(Messages.SendMessage message)
        {
            MemoryStream stream = new MemoryStream();

            message.Send(new StreamWriter(stream) { AutoFlush = true });

            StreamReader reader = new StreamReader(stream);
            stream.Position = 0;

            return reader.ReadToEnd().Trim();
        }
    }
}