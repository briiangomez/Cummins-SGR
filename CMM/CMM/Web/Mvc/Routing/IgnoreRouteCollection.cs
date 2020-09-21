namespace CMM.Web.Mvc.Routing
{
    using System;
    using System.Configuration;

    public class IgnoreRouteCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new IgnoreRouteElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IgnoreRouteElement) element).Name;
        }
    }
}

