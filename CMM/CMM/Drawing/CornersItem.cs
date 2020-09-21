namespace CMM.Drawing
{
    using System;
    using System.Drawing.Drawing2D;

    internal abstract class CornersItem
    {
        private bool visible = true;

        public abstract void addToPath(GraphicsPath path);

        public bool Visible
        {
            get
            {
                return this.visible;
            }
            set
            {
                this.visible = value;
            }
        }
    }
}

