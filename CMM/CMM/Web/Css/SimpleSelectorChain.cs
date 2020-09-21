namespace CMM.Web.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SimpleSelectorChain : List<SimpleSelector>
    {
        public override string ToString()
        {
            return string.Join(string.Empty, (IEnumerable<string>) (from o in this select o.ToString()));
        }

        public string ToStringWithoutPseudoElementSelector()
        {
            return string.Join(string.Empty, (IEnumerable<string>) (from o in this
                where o.Type != SelectorType.PseudoElement
                select o.ToString()));
        }

        public int Specificity
        {
            get
            {
                return this.Sum<SimpleSelector>(((Func<SimpleSelector, int>) (o => o.Specificity)));
            }
        }
    }
}

