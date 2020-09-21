namespace CMM.Drawing.Filters
{
    using System;
    using System.Drawing;

    public class PolaroidFrameFilter : BasicFilter
    {
        private int _borderHeight = 50;
        private int _borderWidth = 20;
        private string _caption = "Caption";
        private Color _captionColor = Color.FromArgb(210, 0, 0, 0);
        private int _height = 100;
        private int _width = 120;
        private bool _withDropShadow = true;

        public override Image ExecuteFilter(Image rawImage)
        {
            Image image = new ResizeFilter { Width = this._width, Height = this._height }.ExecuteFilter(rawImage);
            Bitmap bitmap = new Bitmap(image.Width + this._borderWidth, image.Height + this._borderHeight);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(image, this._borderWidth / 2, 10);
            graphics.DrawRectangle(new Pen(Color.Black), 0, 0, (image.Width + this._borderWidth) - 1, (image.Height + this._borderHeight) - 1);
            int[] numArray = new int[] { 0x80, 0x40, 0x20, 0x10, 14, 12, 10, 8, 6, 4 };
            Font font = null;
            SizeF ef = new SizeF();
            string text = this._caption;
            int num = (this._width + this._borderWidth) / 2;
            int num2 = 10 + this._height;
            for (int i = 0; i < numArray.Length; i++)
            {
                font = new Font("Lucida Handwriting", (float) numArray[i], FontStyle.Bold);
                if (((ushort) graphics.MeasureString(text, font).Width) < ((ushort) this._width))
                {
                    break;
                }
            }
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Center
            };
            SolidBrush brush = new SolidBrush(this._captionColor);
            graphics.DrawString(text, font, brush, new PointF((float) (num + 1), (float) (num2 + 1)), format);
            if (this.WithDropShadow)
            {
                DropShadowFilter filter2 = new DropShadowFilter();
                bitmap = (Bitmap) filter2.ExecuteFilter(bitmap);
            }
            return bitmap;
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

        public bool WithDropShadow
        {
            get
            {
                return this._withDropShadow;
            }
            set
            {
                this._withDropShadow = value;
            }
        }
    }
}

