namespace CMM.Web.Mvc.WebResourceLoader.Configuration
{
    using CMM.Configuration;
    using CMM.Web.Mvc.WebResourceLoader;
    using System;
    using System.Configuration;

    public sealed class WebResourcesSection : StandaloneConfigurationSection
    {
        private static ConfigurationPropertyCollection _properties;
        private static ConfigurationProperty cacheDuration;
        private static ConfigurationProperty compact;
        private static ConfigurationProperty compress;
        private static ConfigurationProperty mode;
        private static ConfigurationProperty references;
        private static string SectionName = "webResources";
        private static ConfigurationProperty version;

        public WebResourcesSection()
        {
            EnsureStaticPropertyBag();
        }

        private static ConfigurationPropertyCollection EnsureStaticPropertyBag()
        {
            if (_properties == null)
            {
                version = new ConfigurationProperty("version", typeof(string), "1.0", ConfigurationPropertyOptions.None);
                mode = new ConfigurationProperty("mode", typeof(CMM.Web.Mvc.WebResourceLoader.Mode), CMM.Web.Mvc.WebResourceLoader.Mode.Release, ConfigurationPropertyOptions.None);
                compact = new ConfigurationProperty("compact", typeof(bool), true, ConfigurationPropertyOptions.None);
                compress = new ConfigurationProperty("compress", typeof(bool), true, ConfigurationPropertyOptions.None);
                cacheDuration = new ConfigurationProperty("cacheDuration", typeof(int), 30, ConfigurationPropertyOptions.None);
                references = new ConfigurationProperty("references", typeof(ReferenceCollection), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsDefaultCollection);
                ConfigurationPropertyCollection propertys2 = new ConfigurationPropertyCollection();
                propertys2.Add(references);
                propertys2.Add(version);
                propertys2.Add(mode);
                propertys2.Add(compact);
                propertys2.Add(compress);
                propertys2.Add(cacheDuration);
                ConfigurationPropertyCollection propertys = propertys2;
                _properties = propertys;
            }
            return _properties;
        }

        public static WebResourcesSection GetSection()
        {
            return (WebResourcesSection) System.Configuration.ConfigurationManager.GetSection(SectionName);
        }

        public static WebResourcesSection GetSection(string fileName)
        {
            WebResourcesSection section = new WebResourcesSection();
            section.GetSection(fileName, SectionName);
            return section;
        }

        [ConfigurationProperty("cacheDuration", DefaultValue=30)]
        public int CacheDuration
        {
            get
            {
                return (int) base[cacheDuration];
            }
            set
            {
                base[cacheDuration] = value;
            }
        }

        [ConfigurationProperty("compact", DefaultValue=true)]
        public bool Compact
        {
            get
            {
                return (bool) base[compact];
            }
            set
            {
                base[compact] = value;
            }
        }

        [ConfigurationProperty("compress", DefaultValue=true)]
        public bool Compress
        {
            get
            {
                return (bool) base[compress];
            }
            set
            {
                base[compress] = value;
            }
        }

        [ConfigurationProperty("mode", DefaultValue=1)]
        public CMM.Web.Mvc.WebResourceLoader.Mode Mode
        {
            get
            {
                return (CMM.Web.Mvc.WebResourceLoader.Mode) base[mode];
            }
            set
            {
                base[mode] = value;
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return EnsureStaticPropertyBag();
            }
        }

        [ConfigurationProperty("references", IsDefaultCollection=true)]
        public ReferenceCollection References
        {
            get
            {
                return (ReferenceCollection) base[references];
            }
        }

        [ConfigurationProperty("version", DefaultValue="1.0.0.0")]
        public string Version
        {
            get
            {
                return (string) base[version];
            }
            set
            {
                base[version] = value;
            }
        }
    }
}

