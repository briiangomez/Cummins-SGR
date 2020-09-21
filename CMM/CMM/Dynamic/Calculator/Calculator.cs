namespace CMM.Dynamic.Calculator
{
    using CMM.Dynamic.Calculator.Evaluate;
    using CMM.Dynamic.Calculator.Parser;
    using System;

    public static class Calculator
    {
        public static string Calculate(string expression)
        {
            string str;
            string str2;
            Token tokens = new Token(expression);
            Evaluator evaluator = new Evaluator(tokens);
            if (!evaluator.Evaluate(out str, out str2))
            {
                throw new CalculateExpression(str2);
            }
            return str;
        }
    }
}

