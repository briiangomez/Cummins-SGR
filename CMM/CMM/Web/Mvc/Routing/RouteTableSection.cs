namespace CMM.Web.Mvc.Routing
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Xml;

    public class RouteTableSection : ConfigurationSection
    {
        public void DeserializeSection(string config)
        {
            this.DeserializeSection(new XmlTextReader(new StringReader(config)));
        }

        protected override void DeserializeSection(XmlReader reader)
        {
            base.DeserializeSection(reader);
        }

        protected override string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
        {
            return base.SerializeSection(parentElement, name, saveMode);
        }

        [ConfigurationProperty("ignores", IsDefaultCollection=false)]
        public IgnoreRouteCollection Ignores
        {
            get
            {
                return (IgnoreRouteCollection) base["ignores"];
            }
        }

        [ConfigurationProperty("routes", IsDefaultCollection=false)]
        public RouteElementCollection Routes
        {
            get
            {
                return (RouteElementCollection) base["routes"];
            }
        }
    }
}

