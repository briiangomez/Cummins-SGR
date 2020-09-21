namespace CMM.Web.Css
{
    using CMM.Web.Css.Meta;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public class Property
    {
        public const string ImportantToken = "!important";
        public const string InitialRegexPattern = @"^initial(\sinitial)*$";
        public const char NameValueSeparator = ':';

        public Property(string name, string value)
        {
            this.Name = name.Trim().ToLower();
            this.Value = value.Trim();
            this.Important = false;
            this.IsInitial = Regex.IsMatch(this.Value, @"^initial(\sinitial)*$");
            this.IsBrowserGenerated = this.Name.StartsWith("-");
        }

        public static IEnumerable<Property> Combine(IEnumerable<Property> properties)
        {
            List<Property> list = new List<Property>();
            IEnumerable<Property> second = from o in properties
                where (o.Meta == null) || string.IsNullOrEmpty(o.Meta.ShorthandName)
                select o;
            IEnumerable<Property> enumerable2 = properties.Except<Property>(second);
            list.AddRange(second);
            foreach (IGrouping<string, Property> grouping in from o in enumerable2 group o by o.Meta.ShorthandName)
            {
                Property property;
                PropertyMeta meta = PropertyMeta.GetMeta(grouping.Key);
                if ((meta.ValueType as ShorthandType).ShorthandRule.TryCombine(grouping, meta, out property))
                {
                    list.Add(property);
                }
                else
                {
                    list.AddRange(grouping);
                }
            }
            return list;
        }

        public override bool Equals(object obj)
        {
            Property property = obj as Property;
            if (property == null)
            {
                return false;
            }
            return (string.Compare(property.Name, this.Name, true) == 0);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        private IEnumerable<Property> NonRecursiveSplit()
        {
            return (this.Meta.ValueType as ShorthandType).ShorthandRule.Split(this, this.Meta);
        }

        public static Property Parse(string str)
        {
            string str2 = str.Trim().TrimEnd(new char[] { ';' }).Trim();
            int index = str2.IndexOf(':');
            if (index <= 0)
            {
                throw new InvalidStructureException(string.Format("Property must contains name and value part, exception str: {0}", str));
            }
            string name = str2.Substring(0, index).Trim();
            if (name.Length == 0)
            {
                throw new InvalidStructureException(string.Format("Property name could not be null, exception str {0}", str));
            }
            if (index == (str2.Length - 1))
            {
                return new Property(name, null);
            }
            string str4 = str2.Substring(index + 1).Trim();
            if (str4.EndsWith("!important", StringComparison.OrdinalIgnoreCase))
            {
                str4 = str4.Substring(0, str4.Length - "!important".Length);
                return new Property(name, str4) { Important = true };
            }
            return new Property(name, str4);
        }

        private IEnumerable<Property> RecursiveSplit()
        {
            List<Property> list = new List<Property> {
                this
            };
            int index = 0;
            while (index < list.Count)
            {
                if (list[index].IsShorthand)
                {
                    list.AddRange(list[index].NonRecursiveSplit());
                    list.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
            return list;
        }

        public IEnumerable<Property> Split(SplitOptions options = 0)
        {
            if (!this.IsShorthand)
            {
                throw new InvalidOperationException("Only short hand property can be splitted.");
            }
            if (options == SplitOptions.NonRecursive)
            {
                return this.NonRecursiveSplit();
            }
            return this.RecursiveSplit();
        }

        public void Standarlize()
        {
            if (this.Meta != null)
            {
                this.Value = this.Meta.ValueType.Standardlize(this.Value);
            }
        }

        public override string ToString()
        {
            return ((this.Name + ":" + this.Value) ?? (string.Empty + (this.Important ? " !important" : string.Empty)));
        }

        public static bool TryParse(string str, out Property property)
        {
            try
            {
                property = Parse(str);
                return true;
            }
            catch
            {
                property = null;
                return false;
            }
        }

        public bool Important { get; set; }

        public bool IsBrowserGenerated { get; set; }

        public bool IsInitial { get; set; }

        public bool IsShorthand
        {
            get
            {
                if (this.Meta == null)
                {
                    return false;
                }
                return (this.Meta.ValueType is ShorthandType);
            }
        }

        internal PropertyMeta Meta
        {
            get
            {
                return PropertyMeta.GetMeta(this.Name);
            }
        }

        public string Name { get; private set; }

        public virtual string Value { get; protected set; }
    }
}

