namespace CMM.Web.Url
{
    using CMM;
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Hosting;

    public static class UrlUtility
    {
        public static string UrlSeparatorChar = "/";

        public static string AddQueryParam(this string source, string key, string value)
        {
            string str;
            if (!((source != null) && source.Contains("?")))
            {
                str = "?";
            }
            else if (source.EndsWith("?") || source.EndsWith("&"))
            {
                str = string.Empty;
            }
            else
            {
                str = "&";
            }
            return (source + str + HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(value));
        }

        public static string Combine(params string[] virtualPaths)
        {
            if (virtualPaths.Length < 1)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < virtualPaths.Length; i++)
            {
                string str = virtualPaths[i];
                if (!string.IsNullOrEmpty(str))
                {
                    if (i > 0)
                    {
                        str = str.TrimStart(new char[] { '/' });
                        builder.Append("/");
                    }
                    if (i < (virtualPaths.Length - 1))
                    {
                        str = str.TrimEnd(new char[] { '/' });
                    }
                    builder.Append(str);
                }
            }
            return builder.ToString();
        }

        public static string CombineQueryString(string baseUrl, params string[] queries)
        {
            string str = string.Join(string.Empty, queries).TrimStart(new char[] { '?' });
            if (!string.IsNullOrEmpty(str))
            {
                if (baseUrl.Contains<char>('?'))
                {
                    return (baseUrl + str);
                }
                return (baseUrl + "?" + str);
            }
            return baseUrl;
        }

        public static string EnsureHttpHead(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }
            if (!Regex.IsMatch(url, @"^\w"))
            {
                return url;
            }
            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return url;
            }
            return ("http://" + url);
        }

        public static string GetVirtualPath(string physicalPath)
        {
            string oldValue = MapPath("~/");
            physicalPath = physicalPath.Replace(oldValue, "");
            physicalPath = physicalPath.Replace(@"\", "/");
            return ("~/" + physicalPath);
        }

        public static string MapPath(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }
            string str = HostingEnvironment.MapPath(url);
            if (str == null)
            {
                str = url.TrimStart(new char[] { '~' }).Replace('/', Path.DirectorySeparatorChar).TrimStart(new char[] { Path.DirectorySeparatorChar });
                str = Path.Combine(Settings.BaseDirectory, str);
            }
            return str;
        }

        public static string RemoveQuery(string url, params string[] names)
        {
            string str = url;
            foreach (string str2 in names)
            {
                str = ReplaceQuery(url, str2, string.Empty);
            }
            return str;
        }

        public static string ReplaceQuery(string url, string name, string newQuery)
        {
            return Regex.Replace(url, string.Format(@"&?\b{0}=[^&]*", name), newQuery, RegexOptions.IgnoreCase);
        }

        public static string ResolveUrl(string relativeUrl)
        {
            if ((HttpContext.Current != null) && relativeUrl.StartsWith("~"))
            {
                string str = (HttpContext.Current.Items["ApplicationPath"] != null) ? HttpContext.Current.Items["ApplicationPath"].ToString() : HttpContext.Current.Request.ApplicationPath;
                if (str == "/")
                {
                    return relativeUrl.Remove(0, 1);
                }
                return (str + relativeUrl.Remove(0, 1));
            }
            return relativeUrl;
        }

        public static string ToHttpAbsolute(string relativeUrl)
        {
            UriBuilder builder = new UriBuilder(HttpContext.Current.Request.Url) {
                Path = VirtualPathUtility.ToAbsolute(relativeUrl),
                Query = null
            };
            return builder.ToString();
        }
    }
}

