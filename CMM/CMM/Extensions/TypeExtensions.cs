namespace CMM.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class TypeExtensions
    {
        private static readonly List<Type> numericalTypes = new List<Type> { typeof(short), typeof(int), typeof(long), typeof(float), typeof(long), typeof(double), typeof(decimal) };

        public static string AssemblyQualifiedNameWithoutVersion(this Type type)
        {
            string[] strArray = type.AssemblyQualifiedName.Split(new char[] { ',' });
            return string.Format("{0},{1}", strArray[0], strArray[1]);
        }

        public static IEnumerable<Type> GetAllChildTypes(this Type type)
        {
            List<Assembly> list = (from a in AppDomain.CurrentDomain.GetAssemblies()
                where !a.GlobalAssemblyCache
                select a).ToList<Assembly>();
            List<Type> list2 = new List<Type>();
            foreach (Assembly assembly in list)
            {
                try
                {
                    list2.AddRange(assembly.GetTypes());
                }
                catch
                {
                }
            }
            return (from p in list2
                where type.IsAssignableFrom(p) && (type != p)
                select p);
        }

        public static bool IsNumericalType(this Type type)
        {
            return numericalTypes.Contains(type);
        }
    }
}

