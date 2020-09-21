namespace CMM.Drawing
{
    using System;

    [Flags]
    public enum Corner
    {
        All = 0xff,
        BottomLeft = 4,
        BottomRight = 8,
        None = 0,
        TopLeft = 1,
        TopRight = 2
    }
}

