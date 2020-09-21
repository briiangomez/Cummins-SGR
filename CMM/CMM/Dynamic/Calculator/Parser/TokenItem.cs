namespace CMM.Dynamic.Calculator.Parser
{
    using System;

    public class TokenItem
    {
        private bool canShortCircuit;
        private bool inOperandFunction;
        internal TokenItems parent;
        private IIFShortCircuit shortCircuit;
        private CMM.Dynamic.Calculator.Parser.TokenDataType tokenDataType;
        private string tokenName;
        private CMM.Dynamic.Calculator.Parser.TokenType tokenType;
        private bool willBeAssigned;

        public TokenItem(string TokenName, CMM.Dynamic.Calculator.Parser.TokenType TokenType, bool InOperandFunction)
        {
            this.inOperandFunction = false;
            this.willBeAssigned = false;
            this.canShortCircuit = false;
            this.shortCircuit = null;
            this.parent = null;
            this.tokenName = TokenName;
            this.tokenType = TokenType;
            this.tokenDataType = CMM.Dynamic.Calculator.Parser.TokenDataType.Token_DataType_None;
            this.inOperandFunction = InOperandFunction;
        }

        public TokenItem(string TokenName, CMM.Dynamic.Calculator.Parser.TokenType TokenType, CMM.Dynamic.Calculator.Parser.TokenDataType TokenDataType, bool InOperandFunction)
        {
            this.inOperandFunction = false;
            this.willBeAssigned = false;
            this.canShortCircuit = false;
            this.shortCircuit = null;
            this.parent = null;
            this.tokenName = TokenName;
            this.tokenType = TokenType;
            this.tokenDataType = TokenDataType;
            this.inOperandFunction = InOperandFunction;
        }

        public override string ToString()
        {
            return this.tokenName;
        }

        public bool CanShortCircuit
        {
            get
            {
                return this.canShortCircuit;
            }
            set
            {
                this.canShortCircuit = value;
            }
        }

        public bool InOperandFunction
        {
            get
            {
                return this.inOperandFunction;
            }
            set
            {
                this.inOperandFunction = value;
            }
        }

        public int OrderOfOperationPrecedence
        {
            get
            {
                switch (this.tokenName.Trim().ToLower())
                {
                    case "^":
                        return 1;

                    case "*":
                        return 2;

                    case "/":
                        return 2;

                    case "%":
                        return 2;

                    case "+":
                        return 3;

                    case "-":
                        return 3;

                    case "<":
                        return 4;

                    case "<=":
                        return 4;

                    case ">":
                        return 4;

                    case ">=":
                        return 4;

                    case "<>":
                        return 4;

                    case "=":
                        return 4;

                    case "and":
                        return 5;

                    case "or":
                        return 6;
                }
                return 0x3e8;
            }
        }

        public TokenItems Parent
        {
            get
            {
                return this.parent;
            }
        }

        public IIFShortCircuit ShortCircuit
        {
            get
            {
                if (!this.canShortCircuit)
                {
                    return null;
                }
                if (this.shortCircuit == null)
                {
                    this.shortCircuit = new IIFShortCircuit(this);
                }
                return this.shortCircuit;
            }
        }

        public CMM.Dynamic.Calculator.Parser.TokenDataType TokenDataType
        {
            get
            {
                return this.tokenDataType;
            }
        }

        public string TokenName
        {
            get
            {
                return this.tokenName;
            }
        }

        public bool TokenName_Boolean
        {
            get
            {
                if (string.IsNullOrEmpty(this.tokenName))
                {
                    return false;
                }
                return (this.tokenName.Trim().ToLower() == "true");
            }
        }

        public DateTime TokenName_DateTime
        {
            get
            {
                DateTime minValue = DateTime.MinValue;
                if (DateTime.TryParse(this.tokenName, out minValue))
                {
                    return minValue;
                }
                return DateTime.MinValue;
            }
        }

        public double TokenName_Double
        {
            get
            {
                double result = 0.0;
                if (double.TryParse(this.tokenName, out result))
                {
                    return result;
                }
                return 0.0;
            }
        }

        public int TokenName_Int
        {
            get
            {
                int result = 0;
                if (int.TryParse(this.tokenName, out result))
                {
                    return result;
                }
                return 0;
            }
        }

        public CMM.Dynamic.Calculator.Parser.TokenType TokenType
        {
            get
            {
                return this.tokenType;
            }
        }

        public bool WillBeAssigned
        {
            get
            {
                return this.willBeAssigned;
            }
            set
            {
                this.willBeAssigned = value;
            }
        }
    }
}

