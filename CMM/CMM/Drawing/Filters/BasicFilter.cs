namespace CMM.Drawing.Filters
{
    using System;
    using System.Drawing;

    public abstract class BasicFilter : IFilter
    {
        private Color _backColor = Color.FromArgb(0, 0, 0, 0);

        protected BasicFilter()
        {
        }

        public abstract Image ExecuteFilter(Image inputImage);
        public virtual Image ExecuteFilterDemo(Image inputImage)
        {
            return this.ExecuteFilter(inputImage);
        }

        public Color BackGroundColor
        {
            get
            {
                return this._backColor;
            }
            set
            {
                this._backColor = value;
            }
        }
    }
}

