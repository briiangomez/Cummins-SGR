namespace CMM.Web.Mvc.Menu.Configuration
{
    using System;
    using System.Configuration;

    public class MenuItemElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new MenuItemElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MenuItemElement) element).Name;
        }

        [ConfigurationProperty("type", IsRequired=false)]
        public string Type
        {
            get
            {
                return (string) base["type"];
            }
            set
            {
                base["type"] = value;
            }
        }
    }
}

