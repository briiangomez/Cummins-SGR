namespace CMM.Web.Css
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidReadingCallException : Exception
    {
        public InvalidReadingCallException()
        {
        }

        public InvalidReadingCallException(string message) : base(message)
        {
        }

        protected InvalidReadingCallException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidReadingCallException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

