namespace CMM.Reflection
{
    using CMM;
    using System;

    public class MemberException : Exception, ICMMException
    {
        public MemberException(string msg) : base(msg)
        {
        }

        public MemberException(string msg, Exception inner) : base(msg, inner)
        {
        }
    }
}

