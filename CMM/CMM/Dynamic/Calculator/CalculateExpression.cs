namespace CMM.Dynamic.Calculator
{
    using CMM;
    using System;

    public class CalculateExpression : Exception, ICMMException
    {
        public CalculateExpression(string msg) : base(msg)
        {
        }

        public CalculateExpression(string msg, Exception inner) : base(msg, inner)
        {
        }
    }
}

