namespace CMM.Drawing.Filters
{
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Resources;

    public class DropShadowFilter : BasicFilter
    {
        public override Image ExecuteFilter(Image inputImage)
        {
            int x = 4;
            int y = 4;
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            ResourceManager manager = new ResourceManager("CMM.Drawing.Filters.Images", executingAssembly);
            Bitmap image = (Bitmap) manager.GetObject("img");
            Bitmap bitmap2 = new Bitmap(inputImage.Width + 6, inputImage.Height + 6);
            Graphics graphics = Graphics.FromImage(bitmap2);
            graphics.DrawImage(inputImage, 0, 0, inputImage.Width, inputImage.Height);
            GraphicsUnit pixel = GraphicsUnit.Pixel;
            Point point = new Point(bitmap2.Width - 6, bitmap2.Height - 6);
            Point point2 = new Point(bitmap2.Width, bitmap2.Height - 6);
            Point point3 = new Point(bitmap2.Width - 6, bitmap2.Height);
            Point[] pointArray2 = new Point[] { point, point2, point3 };
            Point[] destPoints = pointArray2;
            Rectangle srcRect = new Rectangle(image.Width - 6, image.Height - 6, 6, 6);
            graphics.DrawImage(image, destPoints, srcRect, pixel);
            point = new Point(bitmap2.Width - 6, y);
            point2 = new Point(bitmap2.Width, y);
            point3 = new Point(bitmap2.Width - 6, bitmap2.Height - 6);
            pointArray2 = new Point[] { point, point2, point3 };
            destPoints = pointArray2;
            srcRect = new Rectangle(image.Width - 6, 0, 6, image.Height - 6);
            graphics.DrawImage(image, destPoints, srcRect, pixel);
            point = new Point(x, bitmap2.Height - 6);
            point2 = new Point(bitmap2.Width - 6, bitmap2.Height - 6);
            point3 = new Point(x, bitmap2.Height);
            pointArray2 = new Point[] { point, point2, point3 };
            destPoints = pointArray2;
            srcRect = new Rectangle(0, image.Height - 6, image.Width - 6, 6);
            graphics.DrawImage(image, destPoints, srcRect, pixel);
            return bitmap2;
        }
    }
}

