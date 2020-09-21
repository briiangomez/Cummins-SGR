namespace CMM.Globalization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Globalization;

    public interface IElementRepository
    {
        bool Add(Element element);
        void AddCategory(string category, string culture);
        IQueryable<ElementCategory> Categories();
        IQueryable<Element> Elements();
        IQueryable<CultureInfo> EnabledLanguages();
        Element Get(string name, string category, string culture);
        bool Remove(Element element);
        bool RemoveCategory(string category, string culture);
        bool Update(Element element);
    }
}

