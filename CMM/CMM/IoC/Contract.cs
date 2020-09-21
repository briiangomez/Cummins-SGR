namespace CMM.IoC
{
    using System;
    using System.Runtime.CompilerServices;

    public class Contract
    {
        public Contract()
        {
            this.Index = 0x7fffffff;
        }

        public Type ExportType { get; set; }

        public int Index { get; set; }

        public string Name { get; set; }
    }
}

