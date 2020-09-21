namespace CMM.Globalization.Repository
{
    using CMM.Globalization;
    using System;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;    
    using System.Globalization;

    [PartCreationPolicy(CreationPolicy.Shared), Export(typeof(XmlElementRepository))]
    public class XmlElementRepository : IElementRepository, IDisposable
    {
        private string path;
        private ResXResource resx;

        public XmlElementRepository() : this(Path.Combine(Settings.BaseDirectory, "I18N"))
        {
        }

        public XmlElementRepository(string path)
        {
            this.path = path;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (IOException exception)
                {
                    throw new DirectoryCreateException(exception);
                }
            }
            this.resx = new ResXResource(path);
        }

        public bool Add(Element element)
        {
            if ((element != null) && (this.resx.GetElement(element.Name, element.Category, element.Culture) == null))
            {
                this.resx.AddResource(element);
                return true;
            }
            return false;
        }

        public void AddCategory(string category, string culture)
        {
            this.resx.AddCategory(category, culture);
        }

        public IQueryable<ElementCategory> Categories()
        {
            return this.resx.GetCategories().AsQueryable<ElementCategory>();
        }

        public void Dispose()
        {
        }

        public IQueryable<Element> Elements()
        {
            return this.resx.GetElements();
        }

        public IQueryable<CultureInfo> EnabledLanguages()
        {
            return Enumerable.Empty<CultureInfo>().AsQueryable<CultureInfo>();
        }

        public Element Get(string name, string category, string culture)
        {
            return this.resx.GetElement(name, category, culture);
        }

        public bool Remove(Element element)
        {
            if (element == null)
            {
                return false;
            }
            return this.resx.RemoveResource(element);
        }

        public bool RemoveCategory(string category, string culture)
        {
            return this.resx.RemoveCategory(category, culture);
        }

        public bool Update(Element element)
        {
            if (element == null)
            {
                return false;
            }
            return this.resx.UpdateResource(element);
        }
    }
}

