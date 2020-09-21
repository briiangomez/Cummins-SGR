namespace CMM.Web.Css.Meta
{
    using System;

    public class FontFamilyType : PropertyValueType
    {
        public static readonly EnumType GenericFamily = new EnumType("serif | sans-serif | cursive | fantasy | monospace");

        private bool IsFamilyName(string value)
        {
            return true;
        }

        private bool IsGenericFamilyName(string value)
        {
            return GenericFamily.IsValid(value);
        }

        public override bool IsValid(string value)
        {
            return (this.IsFamilyName(value) || this.IsGenericFamilyName(value));
        }

        public override string Standardlize(string value)
        {
            return value.Replace("\"", "'");
        }

        public override string DefaultValue
        {
            get
            {
                return string.Empty;
            }
        }
    }
}

