namespace CMM.Globalization.Repository
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;

    public static class ResXResourceFileHelper
    {
        public static void Parse(string fileName, out string culture, out string category)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Path.GetFileName(fileName));
            if (!fileNameWithoutExtension.Contains('.'))
            {
                culture = fileNameWithoutExtension;
                category = string.Empty;
            }
            else
            {
                var pointIndex = fileNameWithoutExtension.LastIndexOf('.');
                culture = fileNameWithoutExtension.Substring(pointIndex + 1);
                category = fileNameWithoutExtension.Substring(0, pointIndex);
            }
        }
        public static string GetFileName(string category, string culture)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return culture + ".resx";
            }
            else
            {
                return category + "." + culture + ".resx";
            }
        }
    }
}

