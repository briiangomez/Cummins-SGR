namespace CMM.Drawing.Filters
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class ImageWatermarkFilter : WaterMarkFilter
    {
        private float _alpha = 0.3f;
        private Color _transparentColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
        private Image _waterMarkImage;
        public const string ALPHA_TOKEN_NAME = "ALPHA";
        public const string HEIGHT_TOKEN_NAME = "Height";
        public const string WIDTH_TOKEN_NAME = "Width";

        public override Image ExecuteFilter(Image rawImage)
        {
            float num;
            float num2;
            base._height = rawImage.Height;
            base._width = rawImage.Width;
            Bitmap image = new Bitmap(rawImage);
            image.SetResolution(rawImage.HorizontalResolution, rawImage.VerticalResolution);
            Graphics graphics = Graphics.FromImage(image);
            ImageAttributes imageAttr = new ImageAttributes();
            ColorMap map = new ColorMap {
                OldColor = this._transparentColor,
                NewColor = Color.FromArgb(0, 0, 0, 0)
            };
            ColorMap[] mapArray = new ColorMap[] { map };
            imageAttr.SetRemapTable(mapArray, ColorAdjustType.Bitmap);
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            numArray3 = new float[5];
            numArray3[1] = 1f;
            numArray2[1] = numArray3;
            numArray3 = new float[5];
            numArray3[2] = 1f;
            numArray2[2] = numArray3;
            numArray3 = new float[5];
            numArray3[3] = this._alpha;
            numArray2[3] = numArray3;
            numArray3 = new float[5];
            numArray3[4] = 1f;
            numArray2[4] = numArray3;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            base.CalcDrawPosition(this._waterMarkImage.Width, this._waterMarkImage.Height, 0, out num2, out num);
            graphics.DrawImage(this._waterMarkImage, new Rectangle((int) num, (int) num2, this._waterMarkImage.Width, this._waterMarkImage.Height), 0, 0, this._waterMarkImage.Width, this._waterMarkImage.Height, GraphicsUnit.Pixel, imageAttr);
            return image;
        }

        public override Image ExecuteFilterDemo(Image rawImage)
        {
            return null;
        }

        public float Alpha
        {
            get
            {
                return this._alpha;
            }
            set
            {
                if ((value > 1f) || (value < 0f))
                {
                    throw new Exception("Error setting the opacity value");
                }
                this._alpha = value;
            }
        }

        public Color TransparentColor
        {
            get
            {
                return this._transparentColor;
            }
            set
            {
                this._transparentColor = value;
            }
        }

        public Image WaterMarkImage
        {
            get
            {
                return this._waterMarkImage;
            }
            set
            {
                this._waterMarkImage = value;
            }
        }
    }
}

