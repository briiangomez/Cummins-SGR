namespace CMM.Drawing
{
    using System;
    using System.Drawing.Drawing2D;

    internal class CornersArc : CornersItem
    {
        private double height;
        private double startAngle;
        private double sweepAngle;
        private double width;
        private double x;
        private double y;

        public override void addToPath(GraphicsPath path)
        {
            if (base.Visible)
            {
                path.AddArc((float) this.x, (float) this.y, (float) this.width, (float) this.height, (float) this.startAngle, (float) this.sweepAngle);
            }
        }

        public double Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        public double StartAngle
        {
            get
            {
                return this.startAngle;
            }
            set
            {
                this.startAngle = value;
            }
        }

        public double SweepAngle
        {
            get
            {
                return this.sweepAngle;
            }
            set
            {
                this.sweepAngle = value;
            }
        }

        public double Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
    }
}

