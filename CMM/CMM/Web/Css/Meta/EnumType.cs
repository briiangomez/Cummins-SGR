namespace CMM.Web.Css.Meta
{
    using System;

    public class EnumType : PropertyValueType
    {
        private string _defaultValue;
        private string _validItems;

        public EnumType(string items) : this(items, items.Split(new char[] { '|' })[0].Trim())
        {
        }

        public EnumType(string items, string defaultValue)
        {
            this._defaultValue = defaultValue;
            this._validItems = " " + items.Trim() + " ";
        }

        public override bool IsValid(string value)
        {
            return this._validItems.Contains(" " + value.ToLower() + " ");
        }

        public override string DefaultValue
        {
            get
            {
                return this._defaultValue;
            }
        }
    }
}

