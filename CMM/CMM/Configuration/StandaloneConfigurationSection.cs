namespace CMM.Configuration
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Xml.Linq;

    public class StandaloneConfigurationSection : ConfigurationSection
    {
        public virtual void GetSection(string fileName, string sectionName)
        {
            XDocument document = XDocument.Load(fileName);
            base.DeserializeSection((from e in document.Elements()
                where e.Name == sectionName
                select e).First<XElement>().CreateReader());
        }
    }
}

