namespace CMM.Web.Mvc.Html
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public static class TagBuilderUtility
    {
        public static T Build<T>(this HtmlHelper html) where T: IHtmlControl, new()
        {
            return new T();
        }
    }
}

