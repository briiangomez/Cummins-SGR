namespace CMM.Drawing.Filters
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class BoxFilter : BasicFilter
    {
        private int _boxDepth = 10;
        private Color _boxEndColor = Color.LightBlue;
        private Color _boxStartColor = Color.DarkBlue;
        private Image _sidePanelImage = null;
        private Image _topPanelImage = null;

        public override Image ExecuteFilter(Image inputImage)
        {
            Bitmap image = (Bitmap) inputImage;
            double d = 0.52359877559829882;
            int num2 = this._boxDepth;
            int height = image.Height;
            int width = image.Width;
            int num5 = num2;
            int num6 = (int) ((num2 * Math.Cos(d)) + (image.Width * Math.Cos(d)));
            int num7 = (int) ((image.Height + (image.Width * Math.Sin(d))) + (num2 * Math.Sin(d)));
            Bitmap bitmap2 = new Bitmap(num6, num7);
            Graphics graphics = Graphics.FromImage(bitmap2);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.FillRectangle(new SolidBrush(base.BackGroundColor), 0, 0, bitmap2.Width, bitmap2.Height);
            Point point = new Point((int) (num2 * Math.Cos(d)), (int) ((num2 + image.Width) * Math.Sin(d)));
            Point point2 = new Point((int) (num2 * Math.Cos(d)), image.Height + ((int) ((num2 + image.Width) * Math.Sin(d))));
            Point point3 = new Point((int) ((image.Width + num2) * Math.Cos(d)), (int) (num2 * Math.Sin(d)));
            Point[] destPoints = new Point[] { point, point3, point2 };
            graphics.DrawImage(image, destPoints);
            Point point4 = new Point(point3.X - ((int) (num5 * Math.Cos(d))), point3.Y - ((int) (num5 * Math.Sin(d))));
            Point point5 = new Point(point3.X, point3.Y);
            Point point6 = new Point(point.X, point.Y);
            Point point7 = new Point(point.X - ((int) (num2 * Math.Cos(d))), point.Y - ((int) (num2 * Math.Sin(d))));
            Point[] points = new Point[] { point7, point4, point5, point6 };
            LinearGradientBrush brush = new LinearGradientBrush(point5, point7, this._boxStartColor, this._boxEndColor);
            if (this._topPanelImage != null)
            {
                destPoints = new Point[] { point7, point4, point6 };
                graphics.DrawImage(this._topPanelImage, destPoints);
            }
            else
            {
                graphics.FillPolygon(brush, points);
            }
            Point point8 = new Point(point.X, point.Y);
            Point point9 = new Point(point2.X, point2.Y);
            Point point10 = new Point(point2.X - ((int) (num2 * Math.Cos(d))), point2.Y - ((int) (num2 * Math.Sin(d))));
            Point point11 = new Point(point.X - ((int) (num2 * Math.Cos(d))), point.Y - ((int) (num2 * Math.Sin(d))));
            Point[] pointArray2 = new Point[] { point11, point8, point9, point10 };
            LinearGradientBrush brush2 = new LinearGradientBrush(point11, point9, this._boxStartColor, this._boxEndColor);
            if (this._sidePanelImage != null)
            {
                destPoints = new Point[] { point11, point8, point10 };
                graphics.DrawImage(this._sidePanelImage, destPoints);
                return bitmap2;
            }
            graphics.FillPolygon(brush2, pointArray2);
            return bitmap2;
        }

        public int BoxDepth
        {
            get
            {
                return this._boxDepth;
            }
            set
            {
                this._boxDepth = value;
            }
        }

        public Color BoxEndColor
        {
            get
            {
                return this._boxEndColor;
            }
            set
            {
                this._boxEndColor = value;
            }
        }

        public Color BoxStartColor
        {
            get
            {
                return this._boxStartColor;
            }
            set
            {
                this._boxStartColor = value;
            }
        }

        public Image SidePanelImage
        {
            get
            {
                return this._sidePanelImage;
            }
            set
            {
                this._sidePanelImage = value;
            }
        }

        public Image TopPanelImage
        {
            get
            {
                return this._topPanelImage;
            }
            set
            {
                this._topPanelImage = value;
            }
        }
    }
}

