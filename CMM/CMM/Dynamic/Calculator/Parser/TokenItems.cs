namespace CMM.Dynamic.Calculator.Parser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class TokenItems : IEnumerable<TokenItem>, IEnumerable
    {
        private List<TokenItem> items;
        private Token parent = null;

        public TokenItems(Token Parent)
        {
            this.parent = Parent;
            this.items = new List<TokenItem>();
        }

        public void Add(TokenItem TItem)
        {
            this.items.Add(TItem);
            TItem.parent = this;
        }

        public void AddToFront(TokenItem TItem)
        {
            this.items.Insert(0, TItem);
            TItem.parent = this;
        }

        public IEnumerator<TokenItem> GetEnumerator()
        {
            return new TokemItemsEnumerator(this.items);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TokemItemsEnumerator(this.items);
        }

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        public TokenItem this[int index]
        {
            get
            {
                return this.items[index];
            }
        }

        public Token Parent
        {
            get
            {
                return this.parent;
            }
        }
    }
}

