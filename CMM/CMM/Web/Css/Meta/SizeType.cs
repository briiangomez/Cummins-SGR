namespace CMM.Web.Css.Meta
{
    using System;

    public class SizeType : CombinedType
    {
        public static readonly EnumType AbsoluteSizeKeywords = new EnumType("medium | xx-small | x-small | small | large | x-large | xx-large");
        public static readonly EnumType RelativeSizeKeywords = new EnumType("larger | smaller");

        public SizeType() : base(new PropertyValueType[] { AbsoluteSizeKeywords, RelativeSizeKeywords, PropertyValueType.Length })
        {
        }
    }
}

