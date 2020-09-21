namespace CMM.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class GraphicsExtenions
    {
        #region Circle
        /// <summary>
        /// Draws the circle.
        /// </summary>
        /// <param name="gp">The gp.</param>
        /// <param name="pen">The pen.</param>
        /// <param name="radius">The radius.</param>
        public static void DrawCircle(this Graphics gp, Pen pen, int x, int y, int radius)
        {
            gp.DrawCircle(pen, new Rectangle(x, y, radius, radius));
        }
        /// <summary>
        /// Draws the circle.
        /// </summary>
        /// <param name="gp">The gp.</param>
        /// <param name="pen">The pen.</param>
        /// <param name="rect">The rect.</param>
        public static void DrawCircle(this Graphics gp, Pen pen, Rectangle rect)
        {
            gp.DrawEllipse(pen, rect);
        }
        /// <summary>
        /// Fills the circle.
        /// </summary>
        /// <param name="gp">The gp.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="radius">The radius.</param>
        public static void FillCircle(this Graphics gp, Brush brush, int radius)
        {
            gp.FillCircle(brush, new Rectangle(0, 0, radius, radius));
        }
        /// <summary>
        /// Fills the circle.
        /// </summary>
        /// <param name="gp">The gp.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="rect">The rect.</param>
        public static void FillCircle(this Graphics gp, Brush brush, Rectangle rect)
        {
            gp.FillPie(brush, rect, 0, 360);
        }
        #endregion

        #region Round corner Rectangle
        public static void FillRoundedRectangle(this Graphics gp, System.Drawing.Brush brush,
          float x, float y,
          float width, float height, float radius, float border, Color borderColor, Corner corner = Corner.All)
        {
            PointF basePoint = new PointF(x, y);

            //PointF[] roundedRectangle = new PointF[5];
            //roundedRectangle[0].X = basePoint.X;
            //roundedRectangle[0].Y = basePoint.Y;
            //roundedRectangle[1].X = basePoint.X + width;
            //roundedRectangle[1].Y = basePoint.Y;
            //roundedRectangle[2].X = basePoint.X + width;
            //roundedRectangle[2].Y = basePoint.Y + height;
            //roundedRectangle[3].X = basePoint.X;
            //roundedRectangle[3].Y = basePoint.Y + height;
            //roundedRectangle[4].X = basePoint.X;
            //roundedRectangle[4].Y = basePoint.Y;
            var border1 = 0f;
            var border2 = border;
            if (border2 > 1)
            {
                border1 = border2 / 2.0f - 1;
                border2 = border2 - 1;
            }

            var path = GetRoundedPath(x + border1, y + border1, width - border2, height - border2, radius, corner);

            gp.FillPath(brush, path);
            if (border > 0)
            {
                Pen pen = new Pen(borderColor, border);
                gp.DrawPath(pen, path);
                pen.Dispose();
            }

            brush.Dispose();
            //Pen pen = new Pen(System.Drawing.Color.Black,2);
            //gp.DrawPath(pen, path);
        }
        private static GraphicsPath GetRoundedPath(float x, float y, float width, float height, float radius, Corner corner)
        {
            GraphicsPath path = new GraphicsPath();

            if (radius > 0)
            {
                Corners r = new Corners(x, y, width, height, radius, corner);
                r.Execute(path);
            }
            else
            {
                path.AddRectangle(new RectangleF(x, y, width, height));
            }
            path.CloseFigure();
            return path;
        }

        public static void FillRoundedRectangle(this Graphics gp, System.Drawing.Brush brush,
          float x, float y,
          float width, float height, float radius, Corner corner = Corner.All)
        {

            gp.FillRoundedRectangle(brush, x, y, width, height, radius, 0, Color.Transparent, corner);
            //RectangleF rectangle = new RectangleF(x, y, width, height);
            //GraphicsPath path = GetRoundedRect(rectangle, radius, corner);
            //gp.FillPath(brush, path);

            //Pen pen = new Pen(System.Drawing.Color.Black,2);
            //gp.DrawPath(pen, path);
        }

        public static void DrawRoundedRectangle(this Graphics gp, System.Drawing.Pen pen,
          float x, float y,
          float width, float height, float radius, Corner corner = Corner.All)
        {
            RectangleF rectangle = new RectangleF(x, y, width - pen.Width, height - pen.Width);
            GraphicsPath path = GetRoundedRect(rectangle, radius, corner);
            gp.DrawPath(pen, path);
        }

        #region Get the desired Rounded Rectangle path.
        private static GraphicsPath GetRoundedRect(RectangleF baseRect,
           float radius, Corner corner)
        {
            // if corner radius is less than or equal to zero, 

            // return the original rectangle 

            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            // if the corner radius is greater than or equal to 

            // half the width, or height (whichever is shorter) 

            // then return a capsule instead of a lozenge 

            //if (radius >= (System.Math.Min(baseRect.Width, baseRect.Height)) / 2.0)
            //    return GetCapsule(baseRect);

            // create the arc for the rectangle sides and declare 

            // a graphics path object for the drawing 

            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            if ((corner & Corner.TopLeft) == Corner.TopLeft)
            {
                path.AddArc(arc, 180, 90);
            }
            else
            {
                path.AddLine(new PointF(baseRect.X, baseRect.Y), new PointF(radius, baseRect.Y));
            }

            //画多边形边
            path.AddLine(
                new PointF(radius, baseRect.Y),
                new PointF(baseRect.Right - radius, baseRect.Y)
            );

            // top right arc 

            arc.X = baseRect.Right - diameter;
            if ((corner & Corner.TopRight) == Corner.TopRight)
            {
                path.AddArc(arc, 270, 90);
                path.AddLine(
                new PointF(baseRect.Right, radius),
                new PointF(baseRect.Right, baseRect.Bottom - radius));
            }
            else
            {
                path.AddLine(new PointF(baseRect.Right - radius, baseRect.Y), new PointF(baseRect.Right, baseRect.Y));
                path.AddLine(new PointF(baseRect.Right, baseRect.Y), new PointF(baseRect.Right, radius));
            }

            arc.Y = baseRect.Bottom - diameter;
            // bottom right arc 
            if ((corner & Corner.BottomRight) == Corner.BottomRight)
            {
                path.AddArc(arc, 0, 90);
            }
            else
            {
                path.AddLine(new PointF(baseRect.Right, baseRect.Bottom - radius), new PointF(baseRect.Right, baseRect.Bottom));
                path.AddLine(new PointF(baseRect.Right, baseRect.Bottom), new PointF(baseRect.Right - radius, baseRect.Bottom));
            }


            path.AddLine(
            new PointF(baseRect.Right - radius, baseRect.Bottom),
            new PointF(radius, baseRect.Bottom));

            arc.X = baseRect.Left;
            if ((corner & Corner.BottomLeft) == Corner.BottomLeft)
            {
                path.AddArc(arc, 90, 90);
            }
            else
            {
                path.AddLine(new PointF(baseRect.Right - radius, baseRect.Bottom), new PointF(baseRect.X, baseRect.Bottom));
                path.AddLine(new PointF(baseRect.X, baseRect.Bottom), new PointF(baseRect.X, baseRect.Bottom - radius));
            }
            // bottom left arc      

            path.AddLine(
            new PointF(baseRect.X, baseRect.Bottom - radius),
            new PointF(baseRect.Y, radius));

            if ((corner & Corner.TopLeft) != Corner.TopLeft)
            {
                path.AddLine(new PointF(baseRect.X, baseRect.Y), new PointF(baseRect.X, radius));
            }

            path.CloseFigure();
            return path;
        }

        private static GraphicsPath GetCapsule(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    // return horizontal capsule 

                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    // return vertical capsule 

                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    // return circle 

                    path.AddEllipse(baseRect);
                }
            }
            catch
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }
        #endregion

        #endregion

        #region Cut corner Rectangle
        /// <summary>
        /// Fills the cut rectangle.
        /// </summary>
        /// <param name="gp">The gp.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="corner">The corner.</param>
        public static void FillCutRectangle(this Graphics gp, System.Drawing.Brush brush,
          float x, float y,
          float width, float height, float radius, Corner corner = Corner.All)
        {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            GraphicsPath path = GetCutRect(rectangle, radius, corner);
            gp.FillPath(brush, path);
        }

        /// <summary>
        /// Draws the cut rectangle.
        /// </summary>
        /// <param name="gp">The gp.</param>
        /// <param name="pen">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="corner">The corner.</param>
        public static void DrawCutRectangle(this Graphics gp, System.Drawing.Pen pen, int x, int y,
         int width, int height, int radius, Corner corner)
        {
            float fx = Convert.ToSingle(x);
            float fy = Convert.ToSingle(y);
            float fwidth = Convert.ToSingle(width);
            float fheight = Convert.ToSingle(height);
            float fradius = Convert.ToSingle(radius);
            gp.DrawCutRectangle(pen, fx, fy, fwidth, fheight, fradius, corner);
        }
        /// <summary>
        /// Draws the cut rectangle.
        /// </summary>
        /// <param name="gp">The gp.</param>
        /// <param name="pen">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="corner">The corner.</param>
        public static void DrawCutRectangle(this Graphics gp, System.Drawing.Pen pen,
          float x, float y,
          float width, float height, float radius, Corner corner = Corner.All)
        {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            GraphicsPath path = GetCutRect(rectangle, radius, corner);
            gp.DrawPath(pen, path);
        }

        #region Gets the cut Rectangle path.
        private static GraphicsPath GetCutRect(RectangleF baseRect,
      float radius, Corner corner)
        {
            // if corner radius is less than or equal to zero, 

            // return the original rectangle 

            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            // if the corner radius is greater than or equal to 

            // half the width, or height (whichever is shorter) 

            // then return a capsule instead of a lozenge 

            //if (radius >= (System.Math.Min(baseRect.Width, baseRect.Height)) / 2.0)
            //    return GetCapsule(baseRect);

            // create the arc for the rectangle sides and declare 

            // a graphics path object for the drawing 

            //float diameter = radius * 2.0F;
            //SizeF sizeF = new SizeF(diameter, diameter);
            //RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            if ((corner & Corner.TopLeft) == Corner.TopLeft)
            {
                path.AddLine(new PointF(radius, baseRect.Y), new PointF(0, radius));
            }
            else
            {
                path.AddLine(new PointF(baseRect.X, baseRect.Y), new PointF(radius, baseRect.Y));
                path.AddLine(new PointF(baseRect.X, baseRect.Y), new PointF(baseRect.X, radius));
            }

            //画多边形边
            path.AddLine(
                new PointF(radius, baseRect.Y),
                new PointF(baseRect.Right - radius, baseRect.Y)
            );

            // top right corner

            if ((corner & Corner.TopRight) == Corner.TopRight)
            {
                path.AddLine(
                new PointF(baseRect.Right - radius, baseRect.Y),
                new PointF(baseRect.Right, radius));
            }
            else
            {
                path.AddLine(new PointF(baseRect.Right - radius, baseRect.Y), new PointF(baseRect.Right, baseRect.Y));
                path.AddLine(new PointF(baseRect.Right, baseRect.Y), new PointF(baseRect.Right, radius));
            }


            // bottom right arc 
            if ((corner & Corner.BottomRight) == Corner.BottomRight)
            {
                path.AddLine(
                 new PointF(baseRect.Right, baseRect.Bottom - radius),
                 new PointF(baseRect.Right - radius, baseRect.Bottom));
            }
            else
            {
                path.AddLine(new PointF(baseRect.Right, baseRect.Bottom - radius), new PointF(baseRect.Right, baseRect.Bottom));
                path.AddLine(new PointF(baseRect.Right, baseRect.Bottom), new PointF(baseRect.Right - radius, baseRect.Bottom));
            }


            path.AddLine(
            new PointF(baseRect.Right - radius, baseRect.Bottom),
            new PointF(radius, baseRect.Bottom));

            if ((corner & Corner.BottomLeft) == Corner.BottomLeft)
            {
                path.AddLine(
                  new PointF(radius, baseRect.Bottom),
                  new PointF(0, baseRect.Bottom - radius));
            }
            else
            {
                path.AddLine(new PointF(baseRect.Right - radius, baseRect.Bottom), new PointF(baseRect.X, baseRect.Bottom));
                path.AddLine(new PointF(baseRect.X, baseRect.Bottom), new PointF(baseRect.X, baseRect.Bottom - radius));
            }
            // bottom left arc      

            path.AddLine(
            new PointF(baseRect.X, baseRect.Bottom - radius),
            new PointF(baseRect.X, radius));

            if ((corner & Corner.TopLeft) != Corner.TopLeft)
            {
                path.AddLine(new PointF(baseRect.X, baseRect.Y), new PointF(baseRect.X, radius));
            }

            path.CloseFigure();
            return path;
        }
        #endregion
        #endregion
    }
}

