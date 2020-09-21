namespace CMM.Web.Css
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidStructureException : Exception
    {
        public InvalidStructureException()
        {
        }

        public InvalidStructureException(string message) : base(message)
        {
        }

        protected InvalidStructureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidStructureException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

