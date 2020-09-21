namespace CMM.IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ExportContext
    {
        internal ExportContext()
        {
        }

        public ExportContext Export<T>()
        {
            this.Type = typeof(T);
            return this;
        }

        public ExportContext Export(System.Type type)
        {
            this.Type = type;
            return this;
        }

        public void To<TContract>(string contractName = null, int index = 0x7fffffff)
        {
            this.To(typeof(TContract), contractName, index);
        }

        public void To(System.Type contractType, string contractName = null, int index = 0x7fffffff)
        {
            if (this.Type != null)
            {
                ExportProviders.Default.Export(this.Type, contractType, contractName, index);
            }
        }

        private System.Type Type { get; set; }
    }
}

