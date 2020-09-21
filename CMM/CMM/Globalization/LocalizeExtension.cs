namespace CMM.Globalization
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public static class LocalizeExtension
    {
        public static string Localize(this Element element)
        {
            return element.Value.Map(element.Name, element.Category, CultureInfo.GetCultureInfo(element.Culture)).Value;
        }

        public static string Localize(this string source, CultureInfo culture)
        {
            return source.Localize(null, culture);
        }

        public static string Localize(this string source, string category = "")
        {
            return source.Localize(category, Thread.CurrentThread.CurrentCulture);
        }

        public static string Localize(this string source, string category, CultureInfo culture)
        {
            return source.Map(source, category, culture).Value;
        }

        public static Element Map(this string source, string key, string category = "")
        {
            return source.Map(key, category, Thread.CurrentThread.CurrentCulture);
        }

        public static Element Map(this string source, string key, string category, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return Element.Empty;
            }
            IElementRepository defaultRepository = ElementRepository.DefaultRepository;
            Element element = defaultRepository.Get(key, category, culture.Name);
            if (element == null)
            {
                Element element2 = new Element {
                    Name = source,
                    Category = category,
                    Culture = culture.Name,
                    Value = source
                };
                element = element2;
                defaultRepository.Add(element);
            }
            return element;
        }

        public static string Position(this string str, string position)
        {
            return string.Format("<span title=\"{1}\">{0}</span>", str, position);
        }
    }
}

