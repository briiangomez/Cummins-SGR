namespace CMM.Contracts
{
    using CMM;
    using System;

    public class ContractException : Exception, ICMMException
    {
        public ContractException(string msg) : base(msg)
        {
        }

        public ContractException(string msg, Exception inner) : base(msg, inner)
        {
        }
    }
}

