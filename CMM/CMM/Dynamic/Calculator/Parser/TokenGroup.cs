namespace CMM.Dynamic.Calculator.Parser
{
    using CMM.Dynamic.Calculator.Evaluate;
    using System;

    public class TokenGroup
    {
        private CMM.Dynamic.Calculator.Parser.Tokens tokens = null;
        private CMM.Dynamic.Calculator.Parser.Variables variables = null;

        public TokenGroup()
        {
            this.tokens = new CMM.Dynamic.Calculator.Parser.Tokens(this);
            this.variables = new CMM.Dynamic.Calculator.Parser.Variables();
        }

        public bool EvaluateGroup()
        {
            string sValue = "";
            string errorMsg = "";
            bool flag = false;
            foreach (Token token in this.tokens)
            {
                foreach (Variable variable in token.Variables)
                {
                    if (this.variables.VariableExists(variable.VariableName))
                    {
                        variable.VariableValue = this.variables[variable.VariableName].VariableValue;
                    }
                }
                if ((token.TokenItems.Count > 0) && (token.RPNQueue.Count > 0))
                {
                    token.LastErrorMessage = "";
                }
                Evaluator evaluator = new Evaluator(token);
                if (!evaluator.Evaluate(out sValue, out errorMsg))
                {
                    token.LastErrorMessage = errorMsg;
                    flag = true;
                }
            }
            return flag;
        }

        internal void UpdateVariables(Token tk)
        {
            foreach (Variable variable in tk.Variables)
            {
                if (!this.variables.VariableExists(variable.VariableName))
                {
                    this.variables.Add(variable.VariableName);
                }
            }
        }

        public CMM.Dynamic.Calculator.Parser.Tokens Tokens
        {
            get
            {
                return this.tokens;
            }
        }

        public CMM.Dynamic.Calculator.Parser.Variables Variables
        {
            get
            {
                return this.variables;
            }
        }
    }
}

