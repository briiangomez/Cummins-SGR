namespace CMM.Dynamic.Calculator.Parser
{
    using CMM.Dynamic.Calculator.Evaluate;
    using CMM.Dynamic.Calculator.Support;
    using System;
    using System.Runtime.InteropServices;

    public class IIFShortCircuit
    {
        private TokenItem parent;
        private ExQueue<TokenItem> rpn_condition = null;
        private ExQueue<TokenItem> rpn_false = null;
        private ExQueue<TokenItem> rpn_true = null;

        public IIFShortCircuit(TokenItem Parent)
        {
            this.parent = Parent;
            this.rpn_condition = new ExQueue<TokenItem>();
            this.rpn_true = new ExQueue<TokenItem>();
            this.rpn_false = new ExQueue<TokenItem>();
        }

        public TokenItem Evaluate(out string ErrorMsg)
        {
            ErrorMsg = "";
            string sValue = "";
            Token parent = this.Parent.Parent.Parent;
            Evaluator evaluator = null;
            try
            {
                evaluator = new Evaluator(parent);
            }
            catch (Exception exception)
            {
                ErrorMsg = "Failed to evaluation the condition in IIFShortCircuit(): " + exception.Message;
                return null;
            }
            if (!evaluator.Evaluate(this.rpn_condition, out sValue, out ErrorMsg))
            {
                return null;
            }
            ExQueue<TokenItem> rPNQueue = null;
            if (sValue.Trim().ToLower() == "true")
            {
                rPNQueue = this.rpn_true;
            }
            else
            {
                rPNQueue = this.rpn_false;
            }
            if (!evaluator.Evaluate(rPNQueue, out sValue, out ErrorMsg))
            {
                return null;
            }
            return new TokenItem(sValue, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
        }

        public TokenItem Parent
        {
            get
            {
                return this.parent;
            }
        }

        public ExQueue<TokenItem> RPNCondition
        {
            get
            {
                return this.rpn_condition;
            }
            set
            {
                this.rpn_condition = value;
            }
        }

        public ExQueue<TokenItem> RPNFalse
        {
            get
            {
                return this.rpn_false;
            }
            set
            {
                this.rpn_false = value;
            }
        }

        public ExQueue<TokenItem> RPNTrue
        {
            get
            {
                return this.rpn_true;
            }
            set
            {
                this.rpn_true = value;
            }
        }
    }
}

