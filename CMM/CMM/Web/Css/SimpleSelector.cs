namespace CMM.Web.Css
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class SimpleSelector
    {
        public const string ClassSelectorPrefix = ".";
        public const string IDSelectorPrefix = "#";
        public const string PseudoClassSelectorPrefix = ":";
        public static readonly string[] PseudoElementSelectorNames = new string[] { "first-line", "first-letter", "before", "after" };
        public const string PseudoElementSelectorPrefix = "::";
        public const string UniversalSelectorToken = "*";

        public SimpleSelector(SelectorType type, string name)
        {
            this.Type = type;
            this.Name = name;
        }

        public static implicit operator string(SimpleSelector selector)
        {
            return selector.ToString();
        }

        public static implicit operator SimpleSelector(string str)
        {
            return Parse(str);
        }

        public static SimpleSelector Parse(string str)
        {
            string str2 = str.Trim();
            if (str2 == "*")
            {
                return new SimpleSelector(SelectorType.Universal, "*");
            }
            if (str2.StartsWith("#"))
            {
                return new SimpleSelector(SelectorType.ID, str2.Substring("#".Length));
            }
            if (str2.StartsWith("."))
            {
                return new SimpleSelector(SelectorType.Class, str2.Substring(".".Length));
            }
            if (str2.StartsWith("::"))
            {
                return new SimpleSelector(SelectorType.PseudoElement, str2.Substring("::".Length));
            }
            if (str2.StartsWith(":"))
            {
                string str3 = str2.Substring(":".Length);
                if (PseudoElementSelectorNames.Contains<string>(str3))
                {
                    return new SimpleSelector(SelectorType.PseudoElement, str3);
                }
                return new SimpleSelector(SelectorType.PseudoClass, str3);
            }
            if (str2[0] == '[')
            {
                return AttributeSelector.Parse(str2);
            }
            return new SimpleSelector(SelectorType.Type, str2);
        }

        public override string ToString()
        {
            switch (this.Type)
            {
                case SelectorType.Type:
                    return this.Name;

                case SelectorType.Universal:
                    return "*";

                case SelectorType.ID:
                    return ("#" + this.Name);

                case SelectorType.Class:
                    return ("." + this.Name);

                case SelectorType.PseudoClass:
                    return (":" + this.Name);

                case SelectorType.PseudoElement:
                    return ("::" + this.Name);
            }
            throw new NotSupportedException();
        }

        public string Name { get; set; }

        public int Specificity
        {
            get
            {
                switch (this.Type)
                {
                    case SelectorType.Type:
                    case SelectorType.PseudoElement:
                        return 1;

                    case SelectorType.ID:
                        return 100;

                    case SelectorType.Class:
                    case SelectorType.Attribute:
                    case SelectorType.PseudoClass:
                        return 10;
                }
                return 0;
            }
        }

        public SelectorType Type { get; private set; }
    }
}

