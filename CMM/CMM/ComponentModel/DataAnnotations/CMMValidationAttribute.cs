namespace CMM.ComponentModel.DataAnnotations
{
    using CMM.Globalization;
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false)]
    public class CMMValidationAttribute : ValidationAttribute
    {
        public CMMValidationAttribute(string message) : base(() => message.Localize())
        {
            Func<string> func = null;
            if (func == null)
            {
                func = () => message.Localize("");
            }
        }
    }
}

