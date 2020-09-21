namespace CMM.Drawing.Filters
{
    using CMM.Drawing;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class RoundedCornersFilter : BasicFilter
    {
        private float _cornerRadius = 50f;
        public static string ROTATE_DEGREES_TOKEN = "radius";

        public RoundedCornersFilter()
        {
            this.Corner = CMM.Drawing.Corner.All;
            base.BackGroundColor = Color.Transparent;
        }

        public override Image ExecuteFilter(Image inputImage)
        {
            Bitmap image = new Bitmap(inputImage.Width, inputImage.Height);
            image.MakeTransparent();
            Graphics gp = Graphics.FromImage(image);
            gp.SmoothingMode = SmoothingMode.HighQuality;
            gp.Clear(base.BackGroundColor);
            Brush brush = new TextureBrush(inputImage);
            gp.FillRoundedRectangle(brush, -1f, -1f, (float) (inputImage.Width + 1), (float) (inputImage.Height + 1), this.CornerRadius, this.Corner);
            return image;
        }

        public override Image ExecuteFilterDemo(Image inputImage)
        {
            base.BackGroundColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            return this.ExecuteFilter(inputImage);
        }

        public CMM.Drawing.Corner Corner { get; set; }

        public float CornerRadius
        {
            get
            {
                return this._cornerRadius;
            }
            set
            {
                if (value > 0f)
                {
                    this._cornerRadius = value;
                }
                else
                {
                    this._cornerRadius = 0f;
                }
            }
        }
    }
}

