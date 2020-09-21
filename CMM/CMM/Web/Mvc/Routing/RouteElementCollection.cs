namespace CMM.Web.Mvc.Routing
{
    using System;
    using System.Configuration;
    using System.Reflection;

    [ConfigurationCollection(typeof(RouteConfigElement))]
    public class RouteElementCollection : ConfigurationElementCollection
    {
        public void Add(RouteConfigElement url)
        {
            this.BaseAdd(url);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            base.BaseAdd(element, false);
        }

        public void Clear()
        {
            base.BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RouteConfigElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new RouteConfigElement(elementName);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RouteConfigElement) element).Name;
        }

        public int IndexOf(RouteConfigElement url)
        {
            return base.BaseIndexOf(url);
        }

        public void Remove(RouteConfigElement url)
        {
            if (base.BaseIndexOf(url) >= 0)
            {
                base.BaseRemove(url.Name);
            }
        }

        public void Remove(string name)
        {
            base.BaseRemove(name);
        }

        public void RemoveAt(int index)
        {
            base.BaseRemoveAt(index);
        }

        public string AddElementName
        {
            get
            {
                return base.AddElementName;
            }
            set
            {
                base.AddElementName = value;
            }
        }

        public string ClearElementName
        {
            get
            {
                return base.ClearElementName;
            }
            set
            {
                base.AddElementName = value;
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public int Count
        {
            get
            {
                return base.Count;
            }
        }

        public RouteConfigElement this[int index]
        {
            get
            {
                return (RouteConfigElement) base.BaseGet(index);
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public RouteConfigElement this[string Name]
        {
            get
            {
                return (RouteConfigElement) base.BaseGet(Name);
            }
        }

        public string RemoveElementName
        {
            get
            {
                return base.RemoveElementName;
            }
        }
    }
}

