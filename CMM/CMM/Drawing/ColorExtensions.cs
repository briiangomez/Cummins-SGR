namespace CMM.Drawing
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public static class ColorExtensions
    {
        public static Color Dark(this Color color)
        {
            return ControlPaint.Dark(color);
        }

        public static Color Light(this Color color)
        {
            return ControlPaint.Light(color);
        }
    }
}

