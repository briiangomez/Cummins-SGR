namespace CMM.Reflection
{
    using CMM;
    using System;
    using System.Runtime.CompilerServices;

    public class MemberNotFoundException : Exception, ICMMException
    {
        public MemberNotFoundException(string msg) : base(msg)
        {
        }

        public MemberNotFoundException(string msg, Exception inner) : base(msg, inner)
        {
        }

        public string PropertyName { get; set; }

        public System.Type Type { get; set; }
    }
}

