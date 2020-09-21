namespace CMM.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property)]
    public class DataSourceAttribute : Attribute
    {
        public DataSourceAttribute(Type dataSourceType)
        {
            this.DataSourceType = dataSourceType;
        }

        public Type DataSourceType { get; private set; }
    }
}

