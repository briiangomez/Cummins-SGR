namespace CMM.Web.Mvc.Menu.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public class RouteValuesElement : ConfigurationElement
    {
        private Dictionary<string, object> attributes = new Dictionary<string, object>();

        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            this.attributes.Add(name, value);
            return true;
        }

        public Dictionary<string, object> Attributes
        {
            get
            {
                return this.attributes;
            }
        }
    }
}

