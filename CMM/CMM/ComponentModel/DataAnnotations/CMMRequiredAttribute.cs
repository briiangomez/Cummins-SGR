namespace CMM.ComponentModel.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false)]
    public class CMMRequiredAttribute : CMMValidationAttribute
    {
        public CMMRequiredAttribute(string message) : base(message)
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            string str = value as string;
            if (!((str == null) || this.AllowEmptyStrings))
            {
                return (str.Trim().Length != 0);
            }
            return true;
        }

        public bool AllowEmptyStrings { get; set; }
    }
}

