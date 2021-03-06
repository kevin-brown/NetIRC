﻿using System;
using System.Reflection;
using NetIRC.Messages;

namespace NetIRC
{
    internal class RegisteredMessage
    {
        private readonly Client _client;

        internal Type Type
        {
            get;
            private set;
        }
        private readonly Func<ParsedMessage, Client, bool> _checkMessageDelegate;

        public RegisteredMessage(Client client, Type type)
        {
            this._client = client;

            if (!typeof(IReceiveMessage).IsAssignableFrom(type))
                throw new ArgumentException("type must implement IReceiveMessage", "type");
            
            MethodInfo minfo = type.GetMethod("CheckMessage", BindingFlags.Public | BindingFlags.Static);
            this._checkMessageDelegate = (Func<ParsedMessage, Client, bool>)Delegate.CreateDelegate(typeof(Func<ParsedMessage, Client, bool>), minfo);

            this.Type = type;
        }

        public bool CheckMessage(ParsedMessage message)
        {
            return this._checkMessageDelegate(message, this._client);
        }

        public void ProcessMessage(ParsedMessage message)
        {
            IReceiveMessage instance = (IReceiveMessage)Activator.CreateInstance(this.Type);
            instance.ProcessMessage(message, this._client);
        }
    }
}
