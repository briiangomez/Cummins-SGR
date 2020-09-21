namespace CMM.Web.Mvc.WebResourceLoader.Configuration
{
    using System;
    using System.Configuration;
    using System.Reflection;
    using System.Security.Permissions;
    using System.Web;

    [ConfigurationCollection(typeof(FileInfoElement)), AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    public sealed class FileCollection : ConfigurationElementCollection
    {
        private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        public void Add(FileInfoElement namespaceInformation)
        {
            this.BaseAdd(namespaceInformation);
        }

        public void Clear()
        {
            base.BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FileInfoElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FileInfoElement) element).Filename;
        }

        public void Remove(string s)
        {
            base.BaseRemove(s);
        }

        public void RemoveAt(int index)
        {
            base.BaseRemoveAt(index);
        }

        public FileInfoElement this[int index]
        {
            get
            {
                return (FileInfoElement) base.BaseGet(index);
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

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _properties;
            }
        }
    }
}

