namespace CMM.Web.Css.Meta
{
    using System;

    public class AnyType : PropertyValueType
    {
        public override bool IsValid(string value)
        {
            return true;
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

