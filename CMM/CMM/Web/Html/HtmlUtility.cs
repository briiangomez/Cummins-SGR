namespace CMM.Web.Html
{
    using CMM.Web.Url;
    using System;
    using System.Text.RegularExpressions;

    public static class HtmlUtility
    {
        public static string RemoveComment(string html)
        {
            return Regex.Replace(html, @"<!--[\w\W]*?-->", string.Empty);
        }

        public static string RemoveTag(string html)
        {
            return Regex.Replace(html, @"<[\w\W]*?>", string.Empty);
        }

        public static string StylesheetLink(string href)
        {
            return StylesheetLink("stylesheet", href);
        }

        public static string StylesheetLink(string @ref, string href)
        {
            return string.Format("<link rel=\"{0}\" href=\"{1}\">", @ref, UrlUtility.ResolveUrl(href));
        }
    }
}

