namespace CMM.Web
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web;

    public static class StringEx
    {
        public static string HtmlAttributeEncode(this string source)
        {
            return HttpUtility.HtmlAttributeEncode(source);
        }
    }
}

