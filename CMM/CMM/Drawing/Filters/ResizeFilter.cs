namespace CMM.Drawing.Filters
{
    using System;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ResizeFilter : BasicFilter
    {
        private float _aspectRatio;
        private int _height = 50;
        private InterpolationMode _interpolationType = InterpolationMode.Bicubic;
        private bool _keepAspectRatio = false;
        private bool _lockRatioByHeight = false;
        private bool _lockRatioByWidth = true;
        private int _width = 50;
        public static string HEIGHT_PARAM_TOKEN = "height";
        public static string INTERPOLATION_TYPE_TOKEN = "interpolationType";
        public static string WIDTH_PARAM_TOKEN = "width";

        private float CalcAspectRatio(int width, int height)
        {
            if (height != 0)
            {
                return (((float) width) / ((float) height));
            }
            return 0f;
        }

        public override Image ExecuteFilter(Image inputImage)
        {
            if (this._keepAspectRatio)
            {
                this._aspectRatio = this.CalcAspectRatio(inputImage.Width, inputImage.Height);
                if (this._lockRatioByHeight)
                {
                    this._width = (int) (this._aspectRatio * this.Height);
                }
                else
                {
                    this._height = (int) (((float) this._width) / this._aspectRatio);
                }
            }
            Bitmap image = new Bitmap(this._width, this._height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.InterpolationMode = this._interpolationType;
            graphics.DrawImage(inputImage, 0, 0, this._width, this._height);
            return image;
        }

        public Image ExecuteFilterDemo(Image inputImage, NameValueCollection filterProperties)
        {
            return this.ExecuteFilter(inputImage);
        }

        public int Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
                this._lockRatioByWidth = false;
                this._lockRatioByHeight = true;
            }
        }

        public InterpolationMode InterpolationType
        {
            get
            {
                return this._interpolationType;
            }
            set
            {
                this._interpolationType = value;
            }
        }

        public bool KeepAspectRatio
        {
            get
            {
                return this._keepAspectRatio;
            }
            set
            {
                this._keepAspectRatio = value;
            }
        }

        public bool LockRatioByHeight
        {
            get
            {
                return this._lockRatioByHeight;
            }
            set
            {
                this._lockRatioByHeight = value;
                this._keepAspectRatio = true;
                this._lockRatioByWidth = !this._lockRatioByHeight;
            }
        }

        public bool LockRatioByWidth
        {
            get
            {
                return this._lockRatioByWidth;
            }
            set
            {
                this._lockRatioByWidth = value;
                this._keepAspectRatio = true;
                this._lockRatioByHeight = !this._lockRatioByWidth;
            }
        }

        public int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
                this._lockRatioByWidth = true;
                this._lockRatioByHeight = false;
            }
        }
    }
}

