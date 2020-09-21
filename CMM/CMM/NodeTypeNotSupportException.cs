namespace CMM
{
    using System;
    using System.Linq.Expressions;

    public class NodeTypeNotSupportException : Exception, ICMMException
    {
        public NodeTypeNotSupportException(ExpressionType expressionType) : base(expressionType.ToString() + "is not supported yet.")
        {
        }
    }
}

