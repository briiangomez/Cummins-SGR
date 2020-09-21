namespace SGR.Communicator
{
    using CMM.ComponentModel.DataAnnotations;
    using System;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false)]
    public class CMMDateAttribute : CMMRegularExpressionAttribute
    {
        public CMMDateAttribute(string message) : base(message)
        {
        }

        public override bool IsValid(object value)
        {
            return ((value == null) || base.IsValid(value));
        }

        public override string Pattern
        {
            get
            {
                //string str = Regex.Replace(Regex.Replace(Regex.Replace(CommunicatorContext.Current.Portal.MessageContext.LocalizationInfo.CultureInfo.DateTimeFormat.ShortDatePattern, @"\bM+\b", "(0?[1-9]|1[012])"), @"\bd+\b", "(0?[1-9]|[12][0-9]|3[01])"), @"\by+\b", @"(19|20)\d\d");
                string str = "";
                return ("^" + str + "$");
            }
            set
            {
                base.Pattern = value;
            }
        }
    }
}

