namespace CMM.Drawing.Filters
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public class TextWatermarkFilter : WaterMarkFilter
    {
        private int _alpha = 0x4b;
        private bool _automaticTextSizing = false;
        private string _caption = "Test";
        private Color _captionColor = Color.White;
        private int _textSize = 10;
        public const string ALPHA_TOKEN_NAME = "ALPHA";
        public const string HEIGHT_TOKEN_NAME = "Height";
        public const string WIDTH_TOKEN_NAME = "Width";

        public override Image ExecuteFilter(Image rawImage)
        {
            float num3;
            float num4;
            base._width = rawImage.Width;
            base._height = rawImage.Height;
            Bitmap image = new Bitmap(rawImage.Width, rawImage.Height, PixelFormat.Format24bppRgb);
            image.SetResolution(rawImage.HorizontalResolution, rawImage.VerticalResolution);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(rawImage, new Rectangle(0, 0, base._width, base._height), 0, 0, base._width, base._height, GraphicsUnit.Pixel);
            int[] numArray = new int[] { 0x80, 0x40, 0x20, 0x10, 14, 12, 10, 8, 6, 4 };
            Font font = null;
            SizeF ef = new SizeF();
            if (this._automaticTextSizing)
            {
                for (int i = 0; i < numArray.Length; i++)
                {
                    font = new Font("arial", (float) numArray[i], FontStyle.Bold);
                    if (((ushort) graphics.MeasureString(this._caption, font).Width) < ((ushort) base._width))
                    {
                        break;
                    }
                }
            }
            else
            {
                font = new Font("arial", (float) this._textSize, FontStyle.Bold);
            }
            ef = graphics.MeasureString(this._caption, font);
            int yPixelsMargin = (int) (base._height * 0.0002);
            base.CalcDrawPosition((int) ef.Width, (int) ef.Height, yPixelsMargin, out num3, out num4);
            StringFormat format = new StringFormat();
            SolidBrush brush = new SolidBrush(Color.FromArgb(this._alpha, 0, 0, 0));
            graphics.DrawString(this._caption, font, brush, new PointF(num4 + 1f, num3 + 1f), format);
            SolidBrush brush2 = new SolidBrush(Color.FromArgb(this._alpha, this._captionColor.R, this._captionColor.G, this._captionColor.B));
            graphics.DrawString(this._caption, font, brush2, new PointF(num4, num3), format);
            graphics.Dispose();
            return image;
        }

        public override Image ExecuteFilterDemo(Image rawImage)
        {
            this.Caption = "Caption Demo";
            this.TextSize = 0x12;
            this.AutomaticTextSize = false;
            base.Halign = WaterMarkFilter.HAlign.Bottom;
            base.Valign = WaterMarkFilter.VAlign.Right;
            return this.ExecuteFilter(rawImage);
        }

        public bool AutomaticTextSize
        {
            get
            {
                return this._automaticTextSizing;
            }
            set
            {
                this._automaticTextSizing = value;
            }
        }

        public string Caption
        {
            get
            {
                return this._caption;
            }
            set
            {
                this._caption = value;
            }
        }

        public int CaptionAlpha
        {
            get
            {
                return this._alpha;
            }
            set
            {
                this._alpha = value;
            }
        }

        public Color CaptionColor
        {
            get
            {
                return this._captionColor;
            }
            set
            {
                this._captionColor = value;
            }
        }

        public int TextSize
        {
            get
            {
                return this._textSize;
            }
            set
            {
                this._textSize = value;
            }
        }
    }
}

