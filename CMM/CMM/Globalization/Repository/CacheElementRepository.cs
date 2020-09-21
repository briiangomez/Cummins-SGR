namespace CMM.Globalization.Repository
{
    using CMM.Globalization;
    using System;
    using System.Collections;    
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Globalization;

    public class CacheElementRepository : IElementRepository
    {
        private Hashtable CachedElements = new Hashtable();
        private IElementRepository inner;

        public CacheElementRepository(IElementRepository innerRepository)
        {
            this.inner = innerRepository;
        }

        public bool Add(Element element)
        {
            bool flag = this.inner.Add(element);
            if (flag)
            {
                this.AddCache(element);
            }
            return flag;
        }

        public void AddCache(Element element)
        {
            ElementCacheKey key = new ElementCacheKey(element);
            this.CachedElements[key] = element;
        }

        public void AddCategory(string category, string culture)
        {
            this.inner.AddCategory(category, culture);
        }

        public IQueryable<ElementCategory> Categories()
        {
            return this.inner.Categories();
        }

        public void ClearCache()
        {
            lock (this.CachedElements)
            {
                this.CachedElements.Clear();
            }
        }

        public IQueryable<Element> Elements()
        {
            return this.inner.Elements();
        }

        public IQueryable<CultureInfo> EnabledLanguages()
        {
            return this.inner.EnabledLanguages();
        }

        public Element Get(string name, string category, string culture)
        {
            ElementCacheKey key = new ElementCacheKey(name, category, culture);
            Element element = this.CachedElements[key] as Element;
            if (element == null)
            {
                element = this.inner.Get(name, category, culture);
                if (element != null)
                {
                    this.AddCache(element);
                }
            }
            return element;
        }

        public bool Remove(Element element)
        {
            bool flag = this.inner.Remove(element);
            if (flag)
            {
                this.RemoveCache(element);
            }
            return flag;
        }

        public void RemoveCache(Element element)
        {
            ElementCacheKey key = new ElementCacheKey(element);
            lock (this.CachedElements)
            {
                if (this.CachedElements.ContainsKey(key))
                {
                    this.CachedElements.Remove(key);
                }
            }
        }

        public bool RemoveCategory(string category, string culture)
        {
            bool flag = this.inner.RemoveCategory(category, culture);
            this.ClearCache();
            return flag;
        }

        public bool Update(Element element)
        {
            bool flag = this.inner.Update(element);
            if (flag)
            {
                this.RemoveCache(element);
            }
            return flag;
        }
    }
}

