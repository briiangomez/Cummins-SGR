namespace CMM.Web.Mvc.Menu.Configuration
{
    using CMM.Collections;
    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    public class MenuItemElement : ConfigurationElement
    {
        private ReadonlyNameValueCollection properties;

        public MenuItemElement()
        {
            this.properties = new ReadonlyNameValueCollection();
        }

        public MenuItemElement(string elementName)
        {
            this.properties = new ReadonlyNameValueCollection();
            this.Name = elementName;
        }

        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            this.properties[name] = value;
            return true;
        }

        [ConfigurationProperty("action", IsRequired=false)]
        public string Action
        {
            get
            {
                return (string) base["action"];
            }
            set
            {
                base["action"] = value;
            }
        }

        [ConfigurationProperty("area", IsRequired=false)]
        public string Area
        {
            get
            {
                return (string) base["area"];
            }
            set
            {
                base["area"] = value;
            }
        }

        [ConfigurationProperty("controller", IsRequired=false)]
        public string Controller
        {
            get
            {
                return (string) base["controller"];
            }
            set
            {
                base["controller"] = value;
            }
        }

        [ConfigurationProperty("htmlAttributes", IsRequired=false)]
        public RouteValuesElement HtmlAttributes
        {
            get
            {
                return (RouteValuesElement) base["htmlAttributes"];
            }
            set
            {
                base["htmlAttributes"] = value;
            }
        }

        [ConfigurationProperty("initializer", IsRequired=false)]
        public string Initializer
        {
            get
            {
                return (string) base["initializer"];
            }
            set
            {
                base["initializer"] = value;
            }
        }

        [ConfigurationProperty("items", IsDefaultCollection=false)]
        public MenuItemElementCollection Items
        {
            get
            {
                return (MenuItemElementCollection) base["items"];
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

        [ConfigurationProperty("routeValues", IsRequired=false)]
        public RouteValuesElement RouteValues
        {
            get
            {
                return (RouteValuesElement) base["routeValues"];
            }
            set
            {
                base["routeValues"] = value;
            }
        }

        [ConfigurationProperty("text", IsRequired=false)]
        public string Text
        {
            get
            {
                return (string) base["text"];
            }
            set
            {
                base["text"] = value;
            }
        }

        public NameValueCollection UnrecognizedProperties
        {
            get
            {
                this.properties.MakeReadOnly();
                return this.properties;
            }
        }

        [ConfigurationProperty("visible", IsRequired=false, DefaultValue=true)]
        public bool Visible
        {
            get
            {
                return (bool) base["visible"];
            }
            set
            {
                base["visible"] = value;
            }
        }
    }
}

