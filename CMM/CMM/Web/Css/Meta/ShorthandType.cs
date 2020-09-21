namespace CMM.Web.Css.Meta
{
    using System;
    using System.Runtime.CompilerServices;

    public class ShorthandType : PropertyValueType
    {
        private PropertyValueType _defaultValueType;

        public ShorthandType(CMM.Web.Css.Meta.ShorthandRule rule) : this(rule, null)
        {
        }

        public ShorthandType(CMM.Web.Css.Meta.ShorthandRule rule, PropertyValueType defualtValueType)
        {
            this._defaultValueType = defualtValueType;
            this.ShorthandRule = rule;
        }

        public override bool IsValid(string value)
        {
            return this._defaultValueType.IsValid(value);
        }

        public override string DefaultValue
        {
            get
            {
                return this._defaultValueType.DefaultValue;
            }
        }

        public CMM.Web.Css.Meta.ShorthandRule ShorthandRule { get; private set; }
    }
}

