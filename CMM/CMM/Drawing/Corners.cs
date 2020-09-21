namespace CMM.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class Corners
    {
        private float height;
        private List<CornersItem> list = new List<CornersItem>();
        private double radius;
        private float width;
        private float x;
        private float y;

        public Corners(float x, float y, float width, float height, double radius, Corner corner)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.radius = radius;
            this.FillList(corner);
        }

        private void addWithArc(CornersLine line1, CornersLine line2, CornersArc arc, double f1, double f2, double alfa)
        {
            double num = this.radius / Math.Tan(alfa / 2.0);
            double num2 = Math.Sqrt(Math.Pow(line1.X1 - line1.X2, 2.0) + Math.Pow(line1.Y1 - line1.Y2, 2.0));
            double num3 = Math.Sqrt(Math.Pow(line2.X1 - line2.X2, 2.0) + Math.Pow(line2.Y1 - line2.Y2, 2.0));
            double radius = this.radius;
            if ((Math.Abs(num) > (num2 / 2.0)) || (Math.Abs(num) > (num3 / 2.0)))
            {
                if (num < 0.0)
                {
                    num = -Math.Min((double) (num2 / 2.0), (double) (num3 / 2.0));
                }
                else
                {
                    num = Math.Min((double) (num2 / 2.0), (double) (num3 / 2.0));
                }
                radius = num * Math.Tan(alfa / 2.0);
            }
            line1.NewX2 = line1.X2 + (Math.Abs(num) * Math.Cos(f1));
            line1.NewY2 = line1.Y2 + (Math.Abs(num) * Math.Sin(f1));
            line2.NewX1 = line2.X1 + (Math.Abs(num) * Math.Cos(f2));
            line2.NewY1 = line2.Y1 + (Math.Abs(num) * Math.Sin(f2));
            double d = f1 + (alfa / 2.0);
            double num6 = radius / Math.Sin(alfa / 2.0);
            PointF tf = new PointF();
            if (num > 0.0)
            {
                tf.X = (float) (line1.X2 + (num6 * Math.Cos(d)));
                tf.Y = (float) (line1.Y2 + (num6 * Math.Sin(d)));
            }
            else
            {
                tf.X = (float) (line1.X2 - (num6 * Math.Cos(d)));
                tf.Y = (float) (line1.Y2 - (num6 * Math.Sin(d)));
            }
            double num7 = Math.Atan2(line1.NewY2 - tf.Y, line1.NewX2 - tf.X);
            double num8 = Math.Atan2(line2.NewY1 - tf.Y, line2.NewX1 - tf.X);
            double num9 = num7;
            double num10 = num8 - num7;
            if (num10 > 3.1415926535897931)
            {
                num10 = -(6.2831853071795862 - num10);
            }
            else if (num10 < -3.1415926535897931)
            {
                num10 = 6.2831853071795862 + num10;
            }
            arc.X = tf.X - radius;
            arc.Y = tf.Y - radius;
            arc.Width = radius * 2.0;
            arc.Height = radius * 2.0;
            arc.StartAngle = num9 * 57.295779513082323;
            arc.SweepAngle = num10 * 57.295779513082323;
        }

        private static void addWithoutArc(CornersArc arc)
        {
            arc.Visible = false;
        }

        private void CalculateRoundLines(CornersLine line1, CornersLine line2, CornersArc arc)
        {
            double num = Math.Atan2(line1.Y1 - line1.Y2, line1.X1 - line1.X2);
            double num2 = Math.Atan2(line2.Y2 - line2.Y1, line2.X2 - line2.X1);
            double num3 = num2 - num;
            if ((num3 == 0.0) || (Math.Abs(num3) == 3.1415926535897931))
            {
                addWithoutArc(arc);
            }
            else
            {
                this.addWithArc(line1, line2, arc, num, num2, num3);
            }
        }

        public void Execute(GraphicsPath path)
        {
            int num = 0;
            while (true)
            {
                if (num == this.list.Count)
                {
                    break;
                }
                CornersLine line = this.list[num] as CornersLine;
                num++;
                if (num == this.list.Count)
                {
                    break;
                }
                if (this.list[num] is CornersArc)
                {
                    CornersLine line2;
                    CornersArc arc = this.list[num] as CornersArc;
                    num++;
                    if (num == this.list.Count)
                    {
                        line2 = this.list[0] as CornersLine;
                    }
                    else
                    {
                        line2 = this.list[num] as CornersLine;
                    }
                    this.CalculateRoundLines(line, line2, arc);
                }
            }
            for (int i = 0; i < this.list.Count; i++)
            {
                this.list[i].addToPath(path);
            }
        }

        private void FillList(Corner corner)
        {
            CornersLine item = new CornersLine {
                X1 = this.x,
                Y1 = this.y,
                X2 = this.x + this.width,
                Y2 = this.y
            };
            this.list.Add(item);
            if ((corner & Corner.TopRight) == Corner.TopRight)
            {
                this.list.Add(new CornersArc());
            }
            CornersLine line2 = new CornersLine {
                X1 = this.x + this.width,
                Y1 = this.y,
                X2 = this.x + this.width,
                Y2 = this.y + this.height
            };
            this.list.Add(line2);
            if ((corner & Corner.BottomRight) == Corner.BottomRight)
            {
                this.list.Add(new CornersArc());
            }
            CornersLine line3 = new CornersLine {
                X1 = this.x + this.width,
                Y1 = this.y + this.height,
                X2 = this.x,
                Y2 = this.y + this.height
            };
            this.list.Add(line3);
            if ((corner & Corner.BottomLeft) == Corner.BottomLeft)
            {
                this.list.Add(new CornersArc());
            }
            CornersLine line4 = new CornersLine {
                X1 = this.x,
                Y1 = this.y + this.height,
                X2 = this.x,
                Y2 = this.y
            };
            this.list.Add(line4);
            if ((corner & Corner.TopLeft) == Corner.TopLeft)
            {
                this.list.Add(new CornersArc());
            }
        }
    }
}

