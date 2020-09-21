namespace CMM.Dynamic.Calculator.Parser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class TokensEnumerator : IEnumerator<Token>, IDisposable, IEnumerator
    {
        private List<Token> items;
        private int location;

        public TokensEnumerator(List<Token> Items)
        {
            this.items = Items;
            this.location = -1;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            this.location++;
            return (this.location < this.items.Count);
        }

        public void Reset()
        {
            this.location = -1;
        }

        public Token Current
        {
            get
            {
                if ((this.location <= 0) && (this.location >= this.items.Count))
                {
                    throw new InvalidOperationException("The enumerator is out of bounds");
                }
                return this.items[this.location];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                if ((this.location <= 0) && (this.location >= this.items.Count))
                {
                    throw new InvalidOperationException("The enumerator is out of bounds");
                }
                return this.items[this.location];
            }
        }
    }
}

