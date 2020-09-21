namespace CMM.Web.Css.Meta
{
    using System;

    public class UrlType : PropertyValueType
    {
        public const string None = "none";

        private bool IsNone(string value)
        {
            return (value == "none");
        }

        private bool IsUrl(string value)
        {
            return (value.StartsWith("url(", StringComparison.OrdinalIgnoreCase) && value.EndsWith(")"));
        }

        public override bool IsValid(string value)
        {
            value = value.ToLower();
            return (this.IsUrl(value) || this.IsNone(value));
        }

        public override string Standardlize(string value)
        {
            return value.Replace("\"", string.Empty).Replace("'", string.Empty);
        }

        public override string DefaultValue
        {
            get
            {
                return "none";
            }
        }
    }
}

