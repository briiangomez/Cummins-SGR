namespace CMM.ComponentModel.DataAnnotations
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false)]
    public class CMMRegularExpressionAttribute : CMMValidationAttribute
    {
        protected CMMRegularExpressionAttribute(string message) : base(message)
        {
        }

        public CMMRegularExpressionAttribute(string pattern, string message) : base(message)
        {
            this.Pattern = pattern;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            string str = Convert.ToString(value, CultureInfo.CurrentCulture);
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }
            Match match = Regex.Match(str, this.Pattern);
            return ((match.Success && (match.Index == 0)) && (match.Length == str.Length));
        }

        public virtual string Pattern { get; set; }
    }
}

