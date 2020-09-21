namespace CMM.ComponentModel.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false)]
    public class CMMCompareAttribute : CMMValidationAttribute
    {
        public CMMCompareAttribute(string compareTo, string message) : base(message)
        {
            this.CompareTo = compareTo;
        }

        public override bool IsValid(object value)
        {
            return true;
        }

        public string CompareTo { get; set; }
    }
}

