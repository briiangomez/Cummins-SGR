namespace CMM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Runtime.CompilerServices;

    public class ConfigurableCompositionContainer : ExportProvider
    {
        public ConfigurableCompositionContainer() : this(null, new ExportProvider[0])
        {
        }

        public ConfigurableCompositionContainer(params ExportProvider[] providers) : this(null, providers)
        {
        }

        public ConfigurableCompositionContainer(ComposablePartCatalog catalog, params ExportProvider[] providers) : this(catalog, false, providers)
        {
        }

        public ConfigurableCompositionContainer(ComposablePartCatalog catalog, bool isThreadSafe, params ExportProvider[] providers)
        {
            this.CompositionContainer = new System.ComponentModel.Composition.Hosting.CompositionContainer(catalog, isThreadSafe, providers);
        }

        public Lazy<T> GetExportFirstOrConfigured<T>()
        {
            return base.GetExport<T>("__DefaultOrConfigured__");
        }

        public Lazy<T> GetExportFirstOrConfigured<T>(string contractName)
        {
            return base.GetExport<T>("__DefaultOrConfigured__" + contractName);
        }

        protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
        {
            throw new NotImplementedException();
        }

        private System.ComponentModel.Composition.Hosting.CompositionContainer CompositionContainer { get; set; }
    }
}

