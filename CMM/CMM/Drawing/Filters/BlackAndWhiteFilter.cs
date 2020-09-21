namespace CMM.Drawing.Filters
{
    using System;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class BlackAndWhiteFilter : BasicFilter
    {
        private bool _isBright = true;

        public override Image ExecuteFilter(Image inputImage)
        {
            ColorMatrix matrix;
            float[][] numArray;
            float[] numArray2;
            Bitmap image = new Bitmap(inputImage.Width, inputImage.Height);
            Graphics graphics = Graphics.FromImage(image);
            if (this._isBright)
            {
                numArray = new float[6][];
                numArray[0] = new float[] { 0.5f, 0.5f, 0.5f, 0f, 0f };
                numArray[1] = new float[] { 0.5f, 0.5f, 0.5f, 0f, 0f };
                numArray[2] = new float[] { 0.5f, 0.5f, 0.5f, 0f, 0f };
                numArray2 = new float[6];
                numArray2[3] = 1f;
                numArray[3] = numArray2;
                numArray2 = new float[6];
                numArray2[4] = 1f;
                numArray[4] = numArray2;
                numArray2 = new float[6];
                numArray2[5] = 1f;
                numArray[5] = numArray2;
                matrix = new ColorMatrix(numArray);
            }
            else
            {
                numArray = new float[6][];
                numArray[0] = new float[] { 0.3f, 0.3f, 0.3f, 0f, 0f };
                numArray[1] = new float[] { 0.59f, 0.59f, 0.59f, 0f, 0f };
                numArray[2] = new float[] { 0.11f, 0.11f, 0.11f, 0f, 0f };
                numArray2 = new float[6];
                numArray2[3] = 1f;
                numArray[3] = numArray2;
                numArray2 = new float[6];
                numArray2[4] = 1f;
                numArray[4] = numArray2;
                numArray2 = new float[6];
                numArray2[5] = 1f;
                numArray[5] = numArray2;
                matrix = new ColorMatrix(numArray);
            }
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(matrix);
            graphics.DrawImage(inputImage, new Rectangle(0, 0, inputImage.Width, inputImage.Height), 0, 0, inputImage.Width, inputImage.Height, GraphicsUnit.Pixel, imageAttr);
            graphics.Dispose();
            return image;
        }

        public Image ExecuteFilterDemo(Image inputImage, NameValueCollection filterProperties)
        {
            return this.ExecuteFilter(inputImage);
        }

        public bool Brighter
        {
            get
            {
                return this._isBright;
            }
            set
            {
                this._isBright = value;
            }
        }
    }
}

