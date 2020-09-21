namespace CMM.Web.Mvc.WebResourceLoader
{
    using System;
    using System.Collections.Generic;

    internal class ConditionComparer : IEqualityComparer<Condition>
    {
        public bool Equals(Condition x, Condition y)
        {
            return x.If.Equals(y.If);
        }

        public int GetHashCode(Condition obj)
        {
            return obj.GetHashCode();
        }
    }
}

