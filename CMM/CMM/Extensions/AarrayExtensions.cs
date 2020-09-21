namespace CMM.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class AarrayExtensions
    {
        public static string Join(this IEnumerable<string> array, string separator)
        {
            return string.Join(separator, array);
        }
    }
}

