namespace CMM
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class StringExtensions
    {
        public static string Ellipsis(this string str, int length, int ellipsisLength = 3)
        {
            if (str.Length <= length)
            {
                return str;
            }
            return str.Substring(0, length - ellipsisLength).PadRight(length, '.');
        }

        public static bool EqualsOrNullEmpty(this string str1, string str2, StringComparison comparisonType)
        {
            return (string.Compare(str1 ?? "", str2 ?? "", comparisonType) == 0);
        }

        public static string TrimOrNull(this string str)
        {
            if (str == null)
            {
                return str;
            }
            return str.Trim();
        }
    }
}

