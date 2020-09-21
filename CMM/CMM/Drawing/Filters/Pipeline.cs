namespace CMM.Drawing.Filters
{
    using CMM.Drawing;
    using System;
    using System.Drawing;

    public class Pipeline
    {
        private Image _image;

        public Pipeline(Image inputImage)
        {
            this._image = inputImage;
        }

        public Pipeline(string inputFilename)
        {
            this._image = Image.FromFile(inputFilename);
        }

        public Pipeline BlackAndWhite()
        {
            this._image = new BlackAndWhiteFilter().ExecuteFilter(this._image);
            return this;
        }

        public Pipeline CutCorners(float cornerRadius, Color background, Corner roundCorner)
        {
            CutCornersFilter filter2 = new CutCornersFilter {
                Corner = roundCorner
            };
            CutCornersFilter filter = filter2;
            filter.CornerRadius = cornerRadius;
            filter.BackGroundColor = background;
            this._image = filter.ExecuteFilter(this._image);
            return this;
        }

        public Pipeline FloorReflection(float alphaStartValue, float alphaDecreaseRate)
        {
            this._image = new FloorReflectionFilter { AlphaStartValue = alphaStartValue, AlphaDecreaseRate = alphaDecreaseRate }.ExecuteFilter(this._image);
            return this;
        }

        public Pipeline PolariodFrame(string caption)
        {
            this._image = new PolaroidFrameFilter { Caption = caption }.ExecuteFilter(this._image);
            return this;
        }

        public Pipeline Rotate(float Degrees)
        {
            this._image = new RotateFilter { RotateDegrees = Degrees }.ExecuteFilter(this._image);
            return this;
        }

        public Pipeline RoundCorners(float cornerRadius, Color background, Corner roundCorner)
        {
            RoundedCornersFilter filter2 = new RoundedCornersFilter {
                Corner = roundCorner
            };
            RoundedCornersFilter filter = filter2;
            filter.CornerRadius = cornerRadius;
            filter.BackGroundColor = background;
            this._image = filter.ExecuteFilter(this._image);
            return this;
        }

        public Pipeline Skew(int rightShift, int upShift)
        {
            this._image = new SkewFilter { RightShift = rightShift, UpShift = upShift }.ExecuteFilter(this._image);
            return this;
        }

        public Pipeline Watermark(string caption)
        {
            this._image = new TextWatermarkFilter { Caption = caption, AutomaticTextSize = true }.ExecuteFilter(this._image);
            return this;
        }

        public Image CurrentImage
        {
            get
            {
                return this._image;
            }
            set
            {
                this._image = value;
            }
        }
    }
}

