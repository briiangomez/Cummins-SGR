namespace CMM.IoC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ExportProvider : IExportProvider
    {
        private Dictionary<Type, List<Contract>> _Contracts;
        private volatile object _LockObject;
        private ITypeEnumerator TypeEnumerator;

        public ExportProvider()
        {
            this.TypeEnumerator = new CMM.IoC.TypeEnumerator();
            this._LockObject = new object();
        }

        public ExportProvider(ITypeEnumerator typeEnumerator)
        {
            this.TypeEnumerator = new CMM.IoC.TypeEnumerator();
            this._LockObject = new object();
            if (typeEnumerator != null)
            {
                this.TypeEnumerator = typeEnumerator;
            }
        }

        private void AddContract(Dictionary<Type, List<Contract>> list, Type exportType, Type contractType, string contractName, int exportIndex = 0x7fffffff)
        {
            Func<Contract, bool> predicate = null;
            if (contractType == null)
            {
                this.AddContract(list, exportType, exportType, contractName, exportIndex);
            }
            else
            {
                if (!list.ContainsKey(contractType))
                {
                    list.Add(contractType, new List<Contract>());
                }
                if (predicate == null)
                {
                    predicate = i => i.ExportType == exportType;
                }
                if (!list[contractType].Any<Contract>(predicate))
                {
                    Contract item = new Contract {
                        ExportType = exportType,
                        Name = contractName,
                        Index = exportIndex
                    };
                    list[contractType].Add(item);
                }
            }
        }

        public void Export(Type exportType, Type contractType, string contractName, int index)
        {
            this.AddContract(this.Contracts, exportType, contractType, contractName, index);
        }

        private Dictionary<Type, List<Contract>> ExportAll()
        {
            Dictionary<Type, List<Contract>> list = new Dictionary<Type, List<Contract>>();
            IEnumerable<Type> types = this.TypeEnumerator.GetTypes();
            foreach (Type type in types)
            {
                if (type != null)
                {
                    object[] customAttributes = type.GetCustomAttributes(typeof(OrderedExportAttribute), false);
                    foreach (OrderedExportAttribute attribute in customAttributes)
                    {
                        this.AddContract(list, type, attribute.ContractType, attribute.ContractName, attribute.Index);
                    }
                    object[] objArray2 = type.GetCustomAttributes(typeof(ExportAttribute), false);
                    foreach (ExportAttribute attribute2 in objArray2)
                    {
                        this.AddContract(list, type, attribute2.ContractType, attribute2.ContractName, 0x7fffffff);
                    }
                }
            }
            return list;
        }

        public IEnumerable<Contract> FindExports(Type contractType)
        {
            if (this.Contracts.ContainsKey(contractType))
            {
                return this.Contracts[contractType];
            }
            return Enumerable.Empty<Contract>();
        }

        private Dictionary<Type, List<Contract>> Contracts
        {
            get
            {
                if (this._Contracts == null)
                {
                    lock (this._LockObject)
                    {
                        this._Contracts = this.ExportAll();
                    }
                }
                return this._Contracts;
            }
            set
            {
                this._Contracts = value;
            }
        }
    }
}

