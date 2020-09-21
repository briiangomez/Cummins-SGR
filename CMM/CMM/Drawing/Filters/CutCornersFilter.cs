namespace CMM.Drawing.Filters
{
    using CMM.Drawing;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class CutCornersFilter : RoundedCornersFilter
    {
        public override Image ExecuteFilter(Image inputImage)
        {
            Bitmap image = new Bitmap(inputImage.Width, inputImage.Height);
            image.MakeTransparent();
            Graphics gp = Graphics.FromImage(image);
            gp.SmoothingMode = SmoothingMode.HighQuality;
            gp.Clear(base.BackGroundColor);
            Brush brush = new TextureBrush(inputImage);
            gp.FillCutRectangle(brush, 0f, 0f, (float) inputImage.Width, (float) inputImage.Height, base.CornerRadius, base.Corner);
            return image;
        }
    }
}

