namespace CMM.Web.Css.Meta
{
    using System;

    public class LengthType : PropertyValueType
    {
        public const string Units = "em | pt | px | ex | in | cm | mm | pc";
        public const string Zero = "0";

        private bool IsPercentage(string value)
        {
            decimal num;
            return (value.EndsWith("%") && decimal.TryParse(value.Substring(0, value.Length - 2), out num));
        }

        private bool IsUnit(string value)
        {
            decimal num;
            if (value.Length <= 2)
            {
                return false;
            }
            string str = value.Substring(value.Length - 2);
            if (!" em | pt | px | ex | in | cm | mm | pc ".Contains(" " + str + " "))
            {
                return false;
            }
            return decimal.TryParse(value.Substring(0, value.Length - 2), out num);
        }

        public override bool IsValid(string value)
        {
            return ((this.IsUnit(value) || this.IsPercentage(value)) || this.IsZero(value));
        }

        private bool IsZero(string value)
        {
            return (value == "0");
        }

        public override string DefaultValue
        {
            get
            {
                return "0";
            }
        }
    }
}

