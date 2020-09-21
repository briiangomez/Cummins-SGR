namespace CMM.Web
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web;

    public class IHtmlStringComparer : IEqualityComparer<IHtmlString>
    {
        public IHtmlStringComparer() : this(true)
        {
        }

        public IHtmlStringComparer(bool ignoreCase)
        {
            this.IgnoreCase = ignoreCase;
        }

        public bool Equals(IHtmlString x, IHtmlString y)
        {
            return ((x == y) || (string.Compare(x.ToString(), y.ToString(), this.IgnoreCase) == 0));
        }

        public int GetHashCode(IHtmlString obj)
        {
            return obj.ToString().GetHashCode();
        }

        public bool IgnoreCase { get; private set; }
    }
}

