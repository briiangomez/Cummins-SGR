namespace CMM.IoC
{
    using System;
    using System.ComponentModel.Composition;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Class, Inherited=false, AllowMultiple=true)]
    public class OrderedExportAttribute : ExportAttribute
    {
        public OrderedExportAttribute()
        {
        }

        public OrderedExportAttribute(string contractName, int index) : base(contractName)
        {
            this.Index = index;
        }

        public OrderedExportAttribute(Type contractType, int index) : base(contractType)
        {
            this.Index = index;
        }

        public OrderedExportAttribute(string contractName, Type contractType, int index) : base(contractName, contractType)
        {
            this.Index = index;
        }

        public int Index { get; set; }
    }
}

