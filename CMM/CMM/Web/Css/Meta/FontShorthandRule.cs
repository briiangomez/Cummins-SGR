namespace CMM.Web.Css.Meta
{
    using CMM.Web.Css;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class FontShorthandRule : ValueDiscriminationRule
    {
        public FontShorthandRule() : base("[font-style font-variant font-weight] font-size/line-height font-family")
        {
        }

        public override bool TryCombine(IEnumerable<Property> properties, PropertyMeta meta, out Property property)
        {
            property = null;
            return false;
        }
    }
}

