namespace CMM.Drawing.Filters
{
    using System.Drawing;

    public interface IFilter
    {
        Image ExecuteFilter(Image inputImage);
    }
}

