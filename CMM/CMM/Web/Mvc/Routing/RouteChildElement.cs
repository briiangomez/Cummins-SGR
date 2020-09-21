namespace CMM.Web.Mvc.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public class RouteChildElement : ConfigurationElement
    {
        private Dictionary<string, string> attributes = new Dictionary<string, string>();

        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            this.attributes.Add(name, value);
            return true;
        }

        public Dictionary<string, string> Attributes
        {
            get
            {
                return this.attributes;
            }
        }
    }
}

