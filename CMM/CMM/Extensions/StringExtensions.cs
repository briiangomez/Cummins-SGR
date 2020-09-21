namespace CMM.Extensions
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        private static Regex invalidUrlCharacter = new Regex(@"[^a-z|^_|^\d|^\u4e00-\u9fa5]+", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static T As<T>(this string source)
        {
            if (source == null)
            {
                return default(T);
            }
            try
            {
                return (T) Convert.ChangeType(source, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        public static T As<T>(this string source, T defaultValue)
        {
            if (source == null)
            {
                return defaultValue;
            }
            try
            {
                return (T) Convert.ChangeType(source, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        public static object As(this string source, Type type)
        {
            if (source == null)
            {
                return null;
            }
            try
            {
                return Convert.ChangeType(source, type);
            }
            catch
            {
                return null;
            }
        }

        public static bool Contains(this string original, string value, StringComparison comparisionType)
        {
            return (original.IndexOf(value, comparisionType) >= 0);
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return ((source == null) || (source.Length == 0));
        }

        public static string Items(this string source, int itemIndex, string separator = ",")
        {
            if (source != null)
            {
                string[] strArray = source.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length > itemIndex)
                {
                    return strArray[itemIndex];
                }
            }
            return string.Empty;
        }

        public static string NormalizeUrl(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return invalidUrlCharacter.Replace(s, "-");
            }
            return s;
        }

        public static string StripAllTags(this string stringToStrip)
        {
            if (!string.IsNullOrEmpty(stringToStrip))
            {
                stringToStrip = Regex.Replace(stringToStrip, @"</p(?:\s*)>(?:\s*)<p(?:\s*)>", "\n\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                stringToStrip = Regex.Replace(stringToStrip, @"<br(?:\s*)/>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                stringToStrip = Regex.Replace(stringToStrip, "\"", "''", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                stringToStrip = stringToStrip.StripHtmlXmlTags();
            }
            return stringToStrip;
        }

        public static string StripHtmlXmlTags(this string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }
            return Regex.Replace(content, "<[^>]+>?", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
    }
}

