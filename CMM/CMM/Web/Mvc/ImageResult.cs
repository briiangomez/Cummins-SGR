namespace CMM.Web.Mvc
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class ImageResult : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (this.Image == null)
            {
                throw new ArgumentNullException("Image");
            }
            if (this.ImageFormat == null)
            {
                throw new ArgumentNullException("ImageFormat");
            }
            context.HttpContext.Response.Clear();
            if (this.ImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
            {
                context.HttpContext.Response.ContentType = "image/bmp";
            }
            if (this.ImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
            {
                context.HttpContext.Response.ContentType = "image/gif";
            }
            if (this.ImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
            {
                context.HttpContext.Response.ContentType = "image/vnd.microsoft.icon";
            }
            if (this.ImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
            {
                context.HttpContext.Response.ContentType = "image/jpeg";
            }
            if (this.ImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
            {
                context.HttpContext.Response.ContentType = "image/png";
            }
            if (this.ImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
            {
                context.HttpContext.Response.ContentType = "image/tiff";
            }
            if (this.ImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Wmf))
            {
                context.HttpContext.Response.ContentType = "image/wmf";
            }
            this.Image.Save(context.HttpContext.Response.OutputStream, this.ImageFormat);
            if (this.Image != null)
            {
                this.Image.Dispose();
            }
        }

        public System.Drawing.Image Image { get; set; }

        public System.Drawing.Imaging.ImageFormat ImageFormat { get; set; }
    }
}

