namespace CMM.IoC
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> TryGetTypes(this Assembly assembly)
        {
            Type[] typesInAsm;
            try
            {
                typesInAsm = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                typesInAsm = ex.Types;
            }

            foreach (var type in typesInAsm)
            {
                yield return type;
            }
        }
    }
}

