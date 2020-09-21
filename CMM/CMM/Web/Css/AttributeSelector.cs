namespace CMM.Web.Css
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class AttributeSelector : SimpleSelector
    {
        public const char EndQuote = ']';
        public const char StartQuote = '[';
        public static readonly string[] ValidOperators = new string[] { "~=", "|=", "^=", "$=", "*=", "=" };

        public AttributeSelector(string name) : base(SelectorType.Attribute, name)
        {
        }

        public AttributeSelector(string name, string op, string value) : this(name)
        {
            this.Operator = op;
            this.Value = value.Trim(new char[] { '"', '\'' });
        }

        public static implicit operator string(AttributeSelector selector)
        {
            return selector.ToString();
        }

        public static implicit operator AttributeSelector(string str)
        {
            return Parse(str);
        }

        public static AttributeSelector Parse(string str)
        {
            string name = str.Trim().TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' }).Trim();
            for (int i = 0; i < ValidOperators.Length; i++)
            {
                string str3 = ValidOperators[i];
                if (name.IndexOf(str3) >= 0)
                {
                    string[] strArray = name.Split(new string[] { str3 }, StringSplitOptions.None);
                    if (strArray.Length != 2)
                    {
                        throw new InvalidStructureException(string.Format("Invalid attribute selector structure, exception string: \"{0}\".", str));
                    }
                    if (strArray[0].Length == 0)
                    {
                        throw new InvalidStructureException(string.Format("Attribute selector must contains attribute name, execption string: \"{0}\".", str));
                    }
                    return new AttributeSelector(strArray[0], str3, (strArray[1].Length == 0) ? null : strArray[1]);
                }
            }
            return new AttributeSelector(name);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            builder.Append(base.Name);
            if (!string.IsNullOrEmpty(this.Operator))
            {
                builder.Append(this.Operator);
            }
            if (!string.IsNullOrEmpty(this.Value))
            {
                builder.Append('"').Append(this.Value).Append('"');
            }
            builder.Append(']');
            return builder.ToString();
        }

        public string Operator { get; set; }

        public string Value { get; set; }
    }
}

