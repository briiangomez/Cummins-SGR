namespace CMM.Dynamic.Calculator.Parser
{
    using System;
    using System.Collections.Generic;

    public class Variable
    {
        private List<TokenItem> tokenItems;
        private string variableName;
        private string variableValue;

        public Variable(string VarName)
        {
            this.tokenItems = new List<TokenItem>();
            this.variableName = VarName;
            this.variableValue = "";
        }

        public Variable(string VarName, string VarValue)
        {
            this.tokenItems = new List<TokenItem>();
            this.variableName = VarName;
            this.variableValue = VarValue;
        }

        public Variable Clone()
        {
            return new Variable(this.variableName, this.variableValue);
        }

        public string CollectionKey
        {
            get
            {
                return this.variableName.Trim().ToLower();
            }
        }

        public List<TokenItem> TokenItems
        {
            get
            {
                return this.tokenItems;
            }
        }

        public string VariableName
        {
            get
            {
                return this.variableName;
            }
        }

        public string VariableValue
        {
            get
            {
                return this.variableValue;
            }
            set
            {
                this.variableValue = value;
            }
        }
    }
}

