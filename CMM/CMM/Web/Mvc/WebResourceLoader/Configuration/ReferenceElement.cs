namespace CMM.Web.Mvc.WebResourceLoader.Configuration
{
    using System;
    using System.Configuration;

    public class ReferenceElement : ConfigurationElement
    {
        private static ConfigurationProperty files;
        private static ConfigurationProperty mimeType;
        private static ConfigurationProperty nae;
        private static ConfigurationPropertyCollection properties;

        public ReferenceElement()
        {
            EnsureStaticPropertyBag();
        }

        private static ConfigurationPropertyCollection EnsureStaticPropertyBag()
        {
            if (properties == null)
            {
                files = new ConfigurationProperty("", typeof(FileCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
                nae = new ConfigurationProperty("name", typeof(string), string.Empty, ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
                mimeType = new ConfigurationProperty("mimeType", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);
                ConfigurationPropertyCollection propertys = new ConfigurationPropertyCollection();
                propertys.Add(files);
                propertys.Add(nae);
                propertys.Add(mimeType);
                properties = propertys;
            }
            return properties;
        }

        [ConfigurationProperty("", IsDefaultCollection=true)]
        public FileCollection Files
        {
            get
            {
                return (FileCollection) base[files];
            }
        }

        [ConfigurationProperty("mimeType", DefaultValue="text/text")]
        public string MimeType
        {
            get
            {
                return (string) base[mimeType];
            }
            set
            {
                base[mimeType] = value;
            }
        }

        [ConfigurationProperty("name", DefaultValue="")]
        public string Name
        {
            get
            {
                return (string) base[nae];
            }
            set
            {
                base[nae] = value;
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return EnsureStaticPropertyBag();
            }
        }
    }
}

