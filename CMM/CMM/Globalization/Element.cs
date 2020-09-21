namespace CMM.Globalization
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class Element
    {
        private static Element _Empty;

        public virtual CultureInfo GetCultureInfo()
        {
            return CultureInfo.GetCultureInfo(this.Culture);
        }

        public string Category { get; set; }

        public string Culture { get; set; }

        public static Element Empty
        {
            get
            {
                if (_Empty == null)
                {
                    Element element = new Element {
                        Name = "",
                        Value = "",
                        Category = "",
                        Culture = ""
                    };
                    _Empty = element;
                }
                return _Empty;
            }
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}

