namespace CMM
{
    using System;

    public class CMMException : Exception, ICMMException
    {
        public CMMException(string msg) : base(msg)
        {
        }

        public CMMException(string msg, Exception exception) : base(msg, exception)
        {
        }
    }
}

