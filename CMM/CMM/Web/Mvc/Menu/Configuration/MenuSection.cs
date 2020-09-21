namespace CMM.Web.Mvc.Menu.Configuration
{
    using CMM.Configuration;
    using System;
    using System.Configuration;

    public class MenuSection : StandaloneConfigurationSection
    {
        private static string SectionName = "menu";

        public static MenuSection GetSection()
        {
            return (MenuSection) ConfigurationManager.GetSection(SectionName);
        }

        public static MenuSection GetSection(string configFile)
        {
            MenuSection section = new MenuSection();
            section.GetSection(configFile, SectionName);
            return section;
        }

        [ConfigurationProperty("items", IsDefaultCollection=false)]
        public MenuItemElementCollection Items
        {
            get
            {
                return (MenuItemElementCollection) base["items"];
            }
        }
    }
}

