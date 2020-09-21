namespace CMM.Web
{
    using CMM.IO;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web;

    public static class HttpResponseExtensions
    {
        public static void AttachmentHeader(this HttpResponseBase response, string fileName)
        {
            response.ContentType = IOUtility.MimeType(fileName);
            string headerValue = ContentDispositionUtil.GetHeaderValue(HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            response.AddHeader("Content-Disposition", headerValue);
        }
    }
}

