namespace CMM.Drawing.Filters
{
    using System;
    using System.Drawing;

    public class RotateFilter : BasicFilter
    {
        private float _rotateDegrees = -25f;
        public static string ROTATE_DEGREES_TOKEN = "degress";

        public override Image ExecuteFilter(Image inputImage)
        {
            return this.RotateImage(inputImage, this._rotateDegrees);
        }

        private Bitmap RotateImage(Image image, float angle)
        {
            double num9;
            double num10;
            double num11;
            double num12;
            if (image == null)
            {
                return null;
            }
            double width = image.Width;
            double height = image.Height;
            double num3 = (angle * 3.1415926535897931) / 180.0;
            double d = num3;
            while (d < 0.0)
            {
                d += 6.2831853071795862;
            }
            if (((d >= 0.0) && (d < 1.5707963267948966)) || ((d >= 3.1415926535897931) && (d < 4.71238898038469)))
            {
                num9 = Math.Abs(Math.Cos(d)) * width;
                num10 = Math.Abs(Math.Sin(d)) * width;
                num11 = Math.Abs(Math.Cos(d)) * height;
                num12 = Math.Abs(Math.Sin(d)) * height;
            }
            else
            {
                num9 = Math.Abs(Math.Sin(d)) * height;
                num10 = Math.Abs(Math.Cos(d)) * height;
                num11 = Math.Abs(Math.Sin(d)) * width;
                num12 = Math.Abs(Math.Cos(d)) * width;
            }
            double a = num9 + num12;
            double num6 = num11 + num10;
            int num7 = (int) Math.Ceiling(a);
            int num8 = (int) Math.Ceiling(num6);
            Bitmap bitmap = new Bitmap(num7, num8);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Point[] pointArray;
                Point[] pointArray2;
                if ((d >= 0.0) && (d < 1.5707963267948966))
                {
                    pointArray2 = new Point[] { new Point((int) num12, 0), new Point(num7, (int) num10), new Point(0, (int) num11) };
                    pointArray = pointArray2;
                }
                else if ((d >= 1.5707963267948966) && (d < 3.1415926535897931))
                {
                    pointArray2 = new Point[] { new Point(num7, (int) num10), new Point((int) num9, num8), new Point((int) num12, 0) };
                    pointArray = pointArray2;
                }
                else if ((d >= 3.1415926535897931) && (d < 4.71238898038469))
                {
                    pointArray2 = new Point[] { new Point((int) num9, num8), new Point(0, (int) num11), new Point(num7, (int) num10) };
                    pointArray = pointArray2;
                }
                else
                {
                    pointArray2 = new Point[] { new Point(0, (int) num11), new Point((int) num12, 0), new Point((int) num9, num8) };
                    pointArray = pointArray2;
                }
                graphics.FillRectangle(new SolidBrush(base.BackGroundColor), 0, 0, bitmap.Width, bitmap.Height);
                graphics.DrawImage(image, pointArray);
            }
            return bitmap;
        }

        public float RotateDegrees
        {
            get
            {
                return this._rotateDegrees;
            }
            set
            {
                this._rotateDegrees = value;
            }
        }
    }
}

