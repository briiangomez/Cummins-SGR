namespace CMM.Drawing
{
    using System;
    using System.Drawing.Drawing2D;

    internal class CornersLine : CornersItem
    {
        private double newX1;
        private double newX2;
        private double newY1;
        private double newY2;
        private double x1;
        private double x2;
        private double y1;
        private double y2;

        public override void addToPath(GraphicsPath path)
        {
            if (base.Visible && (Math.Sqrt(Math.Pow(this.newX1 - this.newX2, 2.0) + Math.Pow(this.newY1 - this.newY2, 2.0)) > 0.01))
            {
                path.AddLine((float) this.newX1, (float) this.newY1, (float) this.newX2, (float) this.newY2);
            }
        }

        public double NewX1
        {
            get
            {
                return this.newX1;
            }
            set
            {
                this.newX1 = value;
            }
        }

        public double NewX2
        {
            get
            {
                return this.newX2;
            }
            set
            {
                this.newX2 = value;
            }
        }

        public double NewY1
        {
            get
            {
                return this.newY1;
            }
            set
            {
                this.newY1 = value;
            }
        }

        public double NewY2
        {
            get
            {
                return this.newY2;
            }
            set
            {
                this.newY2 = value;
            }
        }

        public double X1
        {
            get
            {
                return this.x1;
            }
            set
            {
                this.x1 = value;
                this.newX1 = value;
            }
        }

        public double X2
        {
            get
            {
                return this.x2;
            }
            set
            {
                this.x2 = value;
                this.newX2 = value;
            }
        }

        public double Y1
        {
            get
            {
                return this.y1;
            }
            set
            {
                this.y1 = value;
                this.newY1 = value;
            }
        }

        public double Y2
        {
            get
            {
                return this.y2;
            }
            set
            {
                this.y2 = value;
                this.newY2 = value;
            }
        }
    }
}

