namespace CMM.Dynamic.Calculator.Parser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class Tokens : IEnumerable<Token>, IEnumerable
    {
        private List<Token> items = new List<Token>();
        private TokenGroup tokenGroup;

        public Tokens(TokenGroup Group)
        {
            this.tokenGroup = Group;
        }

        public bool Add(Token tk)
        {
            this.items.Add(tk);
            tk.TokenGroup = this.tokenGroup;
            this.tokenGroup.UpdateVariables(tk);
            return true;
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public IEnumerator<Token> GetEnumerator()
        {
            return new TokensEnumerator(this.items);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TokensEnumerator(this.items);
        }

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        public Token this[int index]
        {
            get
            {
                return this.items[index];
            }
        }
    }
}

