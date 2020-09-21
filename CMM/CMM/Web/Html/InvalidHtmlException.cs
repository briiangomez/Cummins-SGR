namespace CMM.Web.Html
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidHtmlException : Exception
    {
        public InvalidHtmlException()
        {
        }

        public InvalidHtmlException(string message) : base(message)
        {
        }

        protected InvalidHtmlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidHtmlException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

