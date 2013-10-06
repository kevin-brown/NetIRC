using System;

namespace NetIRC.IRCv3
{
    public sealed class Capability
    {
        public static readonly Capability AwayNotify = new Capability("away-notify");
        
        private readonly String _name;

        private Capability(string name)
        {
            this._name = name;
        }

        public override String ToString()
        {
            return this._name;
        }

        public static explicit operator Capability(string str)
        {
            return new Capability(str);
        }

        public static implicit operator string(Capability capability)
        {
            return capability.ToString();
        }

        public static bool operator ==(Capability x, Capability y)
        {
            if (ReferenceEquals(x, null))
            {
                return ReferenceEquals(y, null);
            }

            return x.Equals(y);
        }

        public static bool operator !=(Capability x, Capability y)
        {
            return !(x == y);
        }

        private bool Equals(Capability other)
        {
            return string.Equals(this._name, other._name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Capability && Equals((Capability)obj);
        }

        public override int GetHashCode()
        {
            return (this._name != null ? this._name.GetHashCode() : 0);
        }
    }
}
