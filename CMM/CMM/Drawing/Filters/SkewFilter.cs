namespace CMM.Drawing.Filters
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class SkewFilter : BasicFilter
    {
        private int _rightShift = -20;
        private int _upShift = 0;
        public const string RIGHT_SHIFT_TOKEN_NAME = "RightShift";
        public const string UP_SHIFT_TOKEN_NAME = "UpShift";

        public override Image ExecuteFilter(Image rawImage)
        {
            Bitmap image = new Bitmap(rawImage.Width + Math.Abs(this._rightShift), rawImage.Height + Math.Abs(this._upShift));
            Graphics graphics = Graphics.FromImage(image);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Point[] destPoints = new Point[3];
            int x = 0;
            int y = 0;
            if (this._rightShift < 0)
            {
                x = this._rightShift * -1;
            }
            if (this._upShift < 0)
            {
                y = this._upShift * -1;
            }
            destPoints[0] = new Point(x + this._rightShift, y);
            destPoints[1] = new Point((x + this._rightShift) + rawImage.Width, y + this._upShift);
            destPoints[2] = new Point(x, y + rawImage.Height);
            try
            {
                graphics.DrawImage(rawImage, destPoints);
            }
            catch
            {
            }
            return image;
        }

        public int RightShift
        {
            get
            {
                return this._rightShift;
            }
            set
            {
                this._rightShift = value;
            }
        }

        public int UpShift
        {
            get
            {
                return this._upShift;
            }
            set
            {
                this._upShift = value;
            }
        }
    }
}

