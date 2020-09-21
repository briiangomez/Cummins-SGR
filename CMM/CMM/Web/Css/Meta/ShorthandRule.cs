namespace CMM.Web.Css.Meta
{
    using CMM.Web.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public abstract class ShorthandRule
    {
        public static readonly BorderPositionShorthandRule BorderProperty = new BorderPositionShorthandRule();
        public static readonly PositionShorthandRule Margin = new PositionShorthandRule();

        protected ShorthandRule()
        {
        }

        public IEnumerable<Property> Split(Property property, PropertyMeta meta)
        {
            if (string.IsNullOrEmpty(property.Value))
            {
                return Enumerable.Empty<Property>();
            }
            string str = property.Value;
            List<string> splitted = new List<string>();
            int num = 0;
            int startIndex = 0;
            ReadStatus separator = ReadStatus.Separator;
            while (num < str.Length)
            {
                char c = str[num];
                if (char.IsWhiteSpace(c))
                {
                    if (separator == ReadStatus.Property)
                    {
                        splitted.Add(str.Substring(startIndex, num - startIndex));
                        separator = ReadStatus.Separator;
                    }
                }
                else if ((c == '"') || (c == '\''))
                {
                    if (separator == ReadStatus.Quote)
                    {
                        separator = ReadStatus.Property;
                    }
                    else
                    {
                        if (separator == ReadStatus.Separator)
                        {
                            startIndex = num;
                        }
                        separator = ReadStatus.Quote;
                    }
                }
                else if (c == ',')
                {
                    separator = ReadStatus.Comma;
                }
                else if (separator == ReadStatus.Separator)
                {
                    startIndex = num;
                    separator = ReadStatus.Property;
                }
                else if (separator == ReadStatus.Comma)
                {
                    separator = ReadStatus.Property;
                }
                num++;
            }
            if (separator == ReadStatus.Property)
            {
                splitted.Add(str.Substring(startIndex, num - startIndex));
            }
            return this.Split(splitted, meta);
        }

        protected abstract IEnumerable<Property> Split(List<string> splitted, PropertyMeta meta);
        public abstract IEnumerable<string> SubProperties(PropertyMeta meta);
        public abstract bool TryCombine(IEnumerable<Property> properties, PropertyMeta meta, out Property property);

        private enum ReadStatus
        {
            Separator,
            Quote,
            Comma,
            Property
        }
    }
}

