namespace CMM.Web.Mvc.WebResourceLoader
{
    using System;
    using System.Runtime.CompilerServices;

    internal class Condition
    {
        public override bool Equals(object obj)
        {
            Condition condition = (Condition) obj;
            return this.If.Equals(condition.If);
        }

        public override int GetHashCode()
        {
            return this.If.GetHashCode();
        }

        public string If { get; set; }
    }
}

