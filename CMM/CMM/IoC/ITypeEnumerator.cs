namespace CMM.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;

    public interface ITypeEnumerator
    {
        IEnumerable<Type> GetTypes();
    }
}

