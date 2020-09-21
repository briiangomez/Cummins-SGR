namespace CMM.IoC
{
    using System;
    using System.Collections.Generic;

    public interface IExportProvider
    {
        void Export(Type exportType, Type contractType, string contractName, int index);
        IEnumerable<Contract> FindExports(Type contractType);
    }
}

