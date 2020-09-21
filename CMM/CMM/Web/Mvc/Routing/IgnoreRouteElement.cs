namespace CMM.Web.Mvc.Routing
{
    using System;
    using System.Configuration;

    public class IgnoreRouteElement : ConfigurationElement
    {
        public IgnoreRouteElement()
        {
        }

        public IgnoreRouteElement(string elementName)
        {
            this.Name = elementName;
        }

        [ConfigurationProperty("constraints", IsRequired=false)]
        public RouteChildElement Constraints
        {
            get
            {
                return (RouteChildElement) base["constraints"];
            }
            set
            {
                base["constraints"] = value;
            }
        }

        [ConfigurationProperty("name", IsRequired=true, IsKey=true)]
        public string Name
        {
            get
            {
                return (string) base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }

        [ConfigurationProperty("url", IsRequired=true)]
        public string Url
        {
            get
            {
                return (string) base["url"];
            }
            set
            {
                base["url"] = value;
            }
        }
    }
}

