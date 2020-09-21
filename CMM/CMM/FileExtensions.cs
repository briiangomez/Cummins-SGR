namespace CMM
{
    using System;

    public class FileExtensions
    {
        public const string Image = ".jpg,.jpeg,.gif,.png,.bmp";
        public static readonly string[] ImageArray = ".jpg,.jpeg,.gif,.png,.bmp".Split(new char[] { ',' });
    }
}

