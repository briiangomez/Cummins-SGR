namespace CMM.IoC
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Web.Compilation;    
    using System.Linq;

    internal class TypeEnumerator : ITypeEnumerator
    {

        public IEnumerable<Type> GetTypes()
        {
            IEnumerable<Type> typesSoFar = Type.EmptyTypes;
            ICollection assemblies = System.Web.Compilation.BuildManager.GetReferencedAssemblies();
            foreach (Assembly assembly in assemblies)
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
                typesSoFar = typesSoFar.Concat(typesInAsm);
            }

            return typesSoFar;
        }
    }
}

