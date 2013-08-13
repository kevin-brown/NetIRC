using System;

namespace NetIRC
{
    [Flags]
    public enum UserRank
    {
        None = 0,
        Voice = 1 << 0,
        HalfOp = 1 << 1,
        Op = 1 << 2,
        Admin = 1 << 3,
        Owner = 1 << 4
    }
}
