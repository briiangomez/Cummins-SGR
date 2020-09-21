namespace CMM.Web.Css
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class RuleSet : Statement
    {
        private SelectorGroup _selectors;
        public const char BlockEndQuote = '}';
        public const char BlockStartQuote = '{';
        public const char PropertyEndToken = ';';
        public const char PropertyNameValueSeparator = ':';

        public static RuleSet Parse(string str)
        {
            string[] strArray = str.Trim().TrimEnd(new char[] { '}' }).Split(new char[] { '{' });
            if (strArray.Length != 2)
            {
                throw new InvalidStructureException(string.Format("Ruleset must contains selector and declaration which quoted by {}, the exception string is {0}", str));
            }
            RuleSet set = new RuleSet();
            foreach (string str3 in strArray[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                set.Selectors.Add(Selector.Parse(str3));
            }
            set.Declaration = CMM.Web.Css.Declaration.Parse(strArray[1]);
            return set;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.Selectors.ToString());
            builder.Append('{');
            builder.Append(this.Declaration.ToString());
            builder.Append('}');
            return builder.ToString();
        }

        public CMM.Web.Css.Declaration Declaration { get; set; }

        public IList<Selector> Selectors
        {
            get
            {
                if (this._selectors == null)
                {
                    this._selectors = new SelectorGroup();
                }
                return this._selectors;
            }
        }
    }
}

