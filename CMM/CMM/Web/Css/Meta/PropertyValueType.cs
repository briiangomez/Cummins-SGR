namespace CMM.Web.Css.Meta
{
    using System;

    public abstract class PropertyValueType
    {
        public static readonly PropertyValueType Any = new AnyType();
        public static readonly ColorType Color = new ColorType();
        public static readonly LengthType Length = new LengthType();
        public static readonly SizeType Size = new SizeType();
        public static readonly UrlType Url = new UrlType();

        protected PropertyValueType()
        {
        }

        public abstract bool IsValid(string value);
        public virtual string Standardlize(string value)
        {
            return value;
        }

        public abstract string DefaultValue { get; }
    }
}

