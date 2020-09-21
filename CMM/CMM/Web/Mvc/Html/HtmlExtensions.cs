namespace CMM.Web.Mvc.Html
{
    using CMM.Web.Url;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.Mvc;

    public static class HtmlExtensions
    {
        public static string ResolveUrl(this HtmlHelper html, string relativeUrl)
        {
            return UrlUtility.ResolveUrl(relativeUrl);
        }

        public static IHtmlString Script(this HtmlHelper html, string scriptUrl)
        {
            string str = html.ResolveUrl(scriptUrl);
            return new HtmlString(string.Format("<script type=\"text/javascript\" src=\"{0}\" ></script>\n", str));
        }

        public static IHtmlString Stylesheet(this HtmlHelper html, string cssUrl)
        {
            string str = html.ResolveUrl(cssUrl);
            return new HtmlString(string.Format("<link type=\"text/css\" rel=\"stylesheet\" href=\"{0}\" />\n", str));
        }

        public static IHtmlString Stylesheet(this HtmlHelper html, string cssUrl, string media)
        {
            string str = html.ResolveUrl(cssUrl);
            return new HtmlString(string.Format("<link type=\"text/css\" rel=\"stylesheet\" href=\"{0}\" media=\"{1}\" />\n", str, media));
        }
    }
}

