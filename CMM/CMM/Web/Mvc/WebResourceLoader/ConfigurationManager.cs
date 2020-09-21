namespace CMM.Web.Mvc.WebResourceLoader
{
    using CMM.Web.Mvc.WebResourceLoader.Configuration;
    using System;
    using System.Collections.Generic;

    public static class ConfigurationManager
    {
        private static IDictionary<string, WebResourcesSection> areasSection = new Dictionary<string, WebResourcesSection>(StringComparer.CurrentCultureIgnoreCase);
        private static WebResourcesSection defaultSection = WebResourcesSection.GetSection();

        public static WebResourcesSection GetSection(string area)
        {
            WebResourcesSection defaultSection = null;
            if (!(string.IsNullOrEmpty(area) || !areasSection.ContainsKey(area)))
            {
                defaultSection = areasSection[area];
            }
            if (defaultSection == null)
            {
                defaultSection = ConfigurationManager.defaultSection;
            }
            if (defaultSection == null)
            {
                throw new WebResourceException("Unable to find web resource configuraion setion.");
            }
            return defaultSection;
        }

        public static void RegisterSection(string area, string configFile)
        {
            lock (areasSection)
            {
                WebResourcesSection section = WebResourcesSection.GetSection(configFile);
                areasSection.Add(area, section);
            }
        }
    }
}

