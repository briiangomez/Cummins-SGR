namespace CMM.Web.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Selector
    {
        private List<Tuple<SelectorCombinator, SimpleSelectorChain>> _chain;
        public const int AttributeSpecificity = 10;
        public const int IDSpecificity = 100;
        public const int ImportantSpecificity = 0x2710;
        public const int InlineSpecificity = 0x3e8;
        public const int TypeSpecificity = 1;

        public void Add(SelectorCombinator combinator, SimpleSelectorChain selector)
        {
            if ((this.Chain.Count > 0) && (combinator == null))
            {
                throw new ArgumentNullException("Combinator could not be null if selector is not in first node of chain.");
            }
            this.Chain.Add(new Tuple<SelectorCombinator, SimpleSelectorChain>(combinator, selector));
        }

        public static implicit operator string(Selector combinator)
        {
            return combinator.ToString();
        }

        public static implicit operator Selector(string str)
        {
            return Parse(str);
        }

        public static Selector Parse(string str)
        {
            Selector selector = new Selector();
            SelectorReader reader = new SelectorReader(str);
            SelectorCombinator combinator = null;
            SimpleSelectorChain chain = new SimpleSelectorChain();
            while (!reader.EndOfStream)
            {
                switch (reader.Status)
                {
                    case SelectorReader.ReadStatus.SimpleSelector:
                    {
                        SimpleSelector item = reader.ReadSimpleSelector();
                        if (item.Type == SelectorType.PseudoElement)
                        {
                            selector.PseudoElementSelector = item;
                        }
                        chain.Add(item);
                        break;
                    }
                    case SelectorReader.ReadStatus.Combinator:
                        selector.Add(combinator, chain);
                        combinator = reader.ReadCombinator();
                        chain = new SimpleSelectorChain();
                        break;
                }
            }
            selector.Add(combinator, chain);
            return selector;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (Tuple<SelectorCombinator, SimpleSelectorChain> tuple in this.Chain)
            {
                if (tuple.Item1 != null)
                {
                    builder.Append((char) tuple.Item1);
                }
                builder.Append(tuple.Item2.ToString());
            }
            return builder.ToString();
        }

        public string ToStringWithoutPseudoElementSelector()
        {
            StringBuilder builder = new StringBuilder();
            foreach (Tuple<SelectorCombinator, SimpleSelectorChain> tuple in this.Chain)
            {
                if (tuple.Item1 != null)
                {
                    builder.Append((char) tuple.Item1);
                }
                builder.Append(tuple.Item2.ToStringWithoutPseudoElementSelector());
            }
            return builder.ToString();
        }

        protected IList<Tuple<SelectorCombinator, SimpleSelectorChain>> Chain
        {
            get
            {
                if (this._chain == null)
                {
                    this._chain = new List<Tuple<SelectorCombinator, SimpleSelectorChain>>();
                }
                return this._chain;
            }
        }

        public SimpleSelector PseudoElementSelector { get; private set; }

        public int Specificity
        {
            get
            {
                return this.Chain.Sum<Tuple<SelectorCombinator, SimpleSelectorChain>>(((Func<Tuple<SelectorCombinator, SimpleSelectorChain>, int>) (o => o.Item2.Specificity)));
            }
        }
    }
}

