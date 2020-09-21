namespace CMM.Globalization
{
    using System;
    using System.Runtime.CompilerServices;

    public class ElementCacheKey
    {
        public ElementCacheKey(Element element) : this(element.Name, element.Category, element.Culture)
        {
        }

        public ElementCacheKey(string name, string category, string culture)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            this.Name = name;
            this.Category = category;
            this.Culture = culture;
            //this.Hash = (("Name:" + this.Name + ";category:" + this.Category) ?? ((";culture:" + culture) ?? "")).GetHashCode();
            this.Hash = ("Name:" + this.Name + ";culture:" + culture).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (this.Hash == ((ElementCacheKey) obj).GetHashCode());
        }

        public override int GetHashCode()
        {
            return this.Hash;
        }

        public string Category { get; private set; }

        public string Culture { get; private set; }

        public int Hash { get; private set; }

        public string Name { get; private set; }
    }
}

