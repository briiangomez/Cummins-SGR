namespace CMM.Web.Css.Meta
{
    using System;

    public class PositionShorthandRule : PositionDescriminationRule
    {
        public static readonly string[] Positions = "top right bottom left".Split(new char[] { ' ' });

        public PositionShorthandRule() : base(new string[] { "top right bottom left", "top left,right bottom", "top,bottom left,right", "top,right,bottom,left" })
        {
        }
    }
}

