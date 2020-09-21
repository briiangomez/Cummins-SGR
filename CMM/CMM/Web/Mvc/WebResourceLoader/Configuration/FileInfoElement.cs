namespace CMM.Web.Mvc.WebResourceLoader.Configuration
{
    using System;
    using System.Configuration;

    public sealed class FileInfoElement : ConfigurationElement
    {
        private static ConfigurationProperty fileName;
        private static ConfigurationProperty @if;
        private static ConfigurationPropertyCollection properties;

        internal FileInfoElement()
        {
            EnsureStaticPropertyBag();
        }

        public FileInfoElement(string filename, string @if) : this()
        {
            this.Filename = filename;
            this.If = @if;
        }

        private static ConfigurationPropertyCollection EnsureStaticPropertyBag()
        {
            if (properties == null)
            {
                fileName = new ConfigurationProperty("filename", typeof(string), null, null, new StringValidator(1), ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
                @if = new ConfigurationProperty("if", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
                ConfigurationPropertyCollection propertys = new ConfigurationPropertyCollection();
                propertys.Add(fileName);
                propertys.Add(@if);
                properties = propertys;
            }
            return properties;
        }

        public override bool Equals(object namespaceInformation)
        {
            FileInfoElement element = namespaceInformation as FileInfoElement;
            return ((element != null) && (this.Filename == element.Filename));
        }

        public override int GetHashCode()
        {
            return this.Filename.GetHashCode();
        }

        [StringValidator(MinLength=1), ConfigurationProperty("filename", IsRequired=true, IsKey=true)]
        public string Filename
        {
            get
            {
                return (string) base[fileName];
            }
            set
            {
                base[fileName] = value;
            }
        }

        [ConfigurationProperty("if", DefaultValue="")]
        public string If
        {
            get
            {
                return (string) base[@if];
            }
            set
            {
                base[@if] = value;
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

