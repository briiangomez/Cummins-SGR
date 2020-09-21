namespace CMM.Web.Css.Meta
{
    using System;
    using System.Linq;

    public class CombinedType : PropertyValueType
    {
        private PropertyValueType[] _subTypes;

        public CombinedType(params PropertyValueType[] subTypes)
        {
            this._subTypes = subTypes;
        }

        public override bool IsValid(string value)
        {
            return this._subTypes.Any<PropertyValueType>(o => o.IsValid(value));
        }

        public override string DefaultValue
        {
            get
            {
                return this._subTypes[0].DefaultValue;
            }
        }
    }
}

