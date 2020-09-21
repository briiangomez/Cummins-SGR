namespace CMM
{
    using System;
    using System.ComponentModel.Composition;
    using System.Runtime.CompilerServices;

    [MetadataAttribute, AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    internal class DefaultExportAttribute : ExportAttribute
    {
        public DefaultExportAttribute()
        {
            this.IsDefault = false;
        }

        public bool IsDefault { get; set; }
    }
}

