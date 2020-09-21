namespace CMM.Drawing.Filters
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class FloorReflectionFilter : BasicFilter
    {
        private float _alphaDecreaseRate = 4f;
        private float _alphaStartValue = 110f;
        private int _height = 200;
        private int _width = 200;
        public const string HEIGHT_TOKEN_NAME = "HEIGHT";
        public const string WIDTH_TOKEN_NAME = "WIDTH";

        public override Image ExecuteFilter(Image rawImage)
        {
            this._width = rawImage.Width;
            this._height = rawImage.Height;
            int num = (int) (this._alphaStartValue / this._alphaDecreaseRate);
            Bitmap image = new Bitmap(this._width, this._height + num);
            Graphics graphics = Graphics.FromImage(image);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(rawImage, 0f, 0f, (float) this._width, (float) this._height);
            Bitmap bitmap2 = (Bitmap) rawImage;
            try
            {
                for (int i = 0; i < (num - 1); i++)
                {
                    for (int j = 0; j < (this._width - 1); j++)
                    {
                        Color pixel = bitmap2.GetPixel(j, (this._height - i) - 1);
                        Color color = Color.FromArgb((pixel.A * (((int) this._alphaStartValue) - (i * ((int) this._alphaDecreaseRate)))) / 0xff, pixel.R, pixel.G, pixel.B);
                        graphics.DrawRectangle(new Pen(color), j, (i + this._height) - 1, 1, 1);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            Pen pen = new Pen(Color.FromArgb(100, 5, 5, 5));
            return image;
        }

        public float AlphaDecreaseRate
        {
            get
            {
                return this._alphaDecreaseRate;
            }
            set
            {
                this._alphaDecreaseRate = value;
            }
        }

        public float AlphaStartValue
        {
            get
            {
                return this._alphaStartValue;
            }
            set
            {
                this._alphaStartValue = value;
            }
        }
    }
}

