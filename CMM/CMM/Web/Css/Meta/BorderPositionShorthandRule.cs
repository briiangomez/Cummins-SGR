namespace CMM.Web.Css.Meta
{
    using System;
    using System.Collections.Generic;

    public class BorderPositionShorthandRule : PositionShorthandRule
    {
        public static readonly Dictionary<string, PropertyValueType> BorderProperties;

        static BorderPositionShorthandRule()
        {
            Dictionary<string, PropertyValueType> dictionary = new Dictionary<string, PropertyValueType>();
            dictionary.Add("width", PropertyValueType.Length);
            dictionary.Add("style", new EnumType("none | hidden | dotted | dashed | solid | double | groove | ridge | inset | outset"));
            dictionary.Add("color", PropertyValueType.Color);
            BorderProperties = dictionary;
        }

        protected override string CombineName(PropertyMeta meta, string name)
        {
            string[] strArray = meta.Name.Split(new char[] { '-' });
            return (strArray[0] + "-" + name + "-" + strArray[1]);
        }
    }
}

