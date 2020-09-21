namespace CMM.Web.Mvc.Routing
{
    using System;
    using System.Configuration;
    using System.Xml;

    public class RouteConfigElement : ConfigurationElement
    {
        public RouteConfigElement()
        {
        }

        public RouteConfigElement(string elementName)
        {
            this.Name = elementName;
        }

        public RouteConfigElement(string newName, string newUrl, string routeHandlerType)
        {
            this.Name = newName;
            this.Url = newUrl;
            this.RouteHandlerType = routeHandlerType;
        }

        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            base.DeserializeElement(reader, serializeCollectionKey);
        }

        protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
        {
            return base.SerializeElement(writer, serializeCollectionKey);
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

        [ConfigurationProperty("dataTokens", IsRequired=false)]
        public RouteChildElement DataTokens
        {
            get
            {
                return (RouteChildElement) base["dataTokens"];
            }
            set
            {
                base["dataTokens"] = value;
            }
        }

        [ConfigurationProperty("defaults", IsRequired=false)]
        public RouteChildElement Defaults
        {
            get
            {
                return (RouteChildElement) base["defaults"];
            }
            set
            {
                base["defaults"] = value;
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

        [ConfigurationProperty("routeHandlerType", IsRequired=false)]
        public string RouteHandlerType
        {
            get
            {
                return (string) base["routeHandlerType"];
            }
            set
            {
                base["routeHandlerType"] = value;
            }
        }

        [ConfigurationProperty("routeType", IsRequired=false)]
        public string RouteType
        {
            get
            {
                return (string) base["routeType"];
            }
            set
            {
                base["routeType"] = value;
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

