namespace CMM.IoC
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class MissingContractException : ApplicationException
    {
        public MissingContractException()
        {
        }

        public MissingContractException(string message) : base(message)
        {
        }

        public MissingContractException(Type type) : this("Can not find any exports for \"" + type.ToString() + "\".")
        {
        }

        protected MissingContractException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public MissingContractException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

