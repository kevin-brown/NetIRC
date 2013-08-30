using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

            if (!type.IsSubclassOf(typeof(IReceiveMessage)))
                throw new ArgumentException("type must implement ReceiveMessage", "type");

            MethodInfo minfo = type.GetMethod("CheckMessage", BindingFlags.Public | BindingFlags.Static);
            this._checkMessageDelegate = (Func<ParsedMessage, Client, bool>)Delegate.CreateDelegate(type, minfo);

            this.Type = type;
        }

        public bool CheckMessage(ParsedMessage message)
        {
            return this._checkMessageDelegate(message, _client);
        }

        public void ProcessMessage(ParsedMessage message)
        {
            IReceiveMessage instance = (IReceiveMessage)Activator.CreateInstance(this.Type);
            instance.ProcessMessage(message, _client);
        }
    }
}
