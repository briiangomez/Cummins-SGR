namespace CMM.Drawing.Filters
{
    using System;
    using System.Runtime.InteropServices;

    public abstract class WaterMarkFilter : BasicFilter
    {
        protected int _height;
        protected int _width;
        public HAlign Halign = HAlign.Bottom;
        public VAlign Valign = VAlign.Center;

        protected WaterMarkFilter()
        {
        }

        protected void CalcDrawPosition(int width, int height, int yPixelsMargin, out float yPosFromBottom, out float xPositionFromLeft)
        {
            if (this.Halign == HAlign.Bottom)
            {
                yPosFromBottom = this._height - height;
            }
            else if (this.Halign == HAlign.Top)
            {
                yPosFromBottom = yPixelsMargin;
            }
            else
            {
                yPosFromBottom = (this._height / 2) - (height / 2);
            }
            if (this.Valign == VAlign.Right)
            {
                xPositionFromLeft = this._width - width;
            }
            else if (this.Valign == VAlign.Left)
            {
                xPositionFromLeft = 0f;
            }
            else
            {
                xPositionFromLeft = (this._width / 2) - (width / 2);
            }
        }

        public enum HAlign
        {
            Top,
            Middle,
            Bottom
        }

        public enum VAlign
        {
            Left,
            Center,
            Right
        }
    }
}

