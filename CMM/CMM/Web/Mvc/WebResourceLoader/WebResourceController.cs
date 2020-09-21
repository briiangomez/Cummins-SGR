namespace CMM.Web.Mvc.WebResourceLoader
{
    using CMM.Web.Mvc.WebResourceLoader.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;


    public class WebResourceController : Controller
    {
        public void Index(string name, string version, string condition)
        {
            HttpResponseBase response = base.Response;
            WebResourcesSection section = WebResourcesSection.GetSection();
            if (section == null)
            {
                throw new HttpException(500, "Unable to find the web resource configuration.");
            }
            ReferenceElement element = section.References[name];
            if (element == null)
            {
                throw new HttpException(500, string.Format("Unable to find any matching web resource settings for {0}.", name));
            }
            Condition condition3 = new Condition {
                If = condition ?? string.Empty
            };
            Condition condition2 = condition3;
            IList<FileInfoElement> list = new List<FileInfoElement>();
            foreach (FileInfoElement element2 in element.Files)
            {
                if (element2.If.Equals(condition2.If))
                {
                    list.Add(element2);
                }
            }
            response.ContentType = element.MimeType;
            Stream outputStream = response.OutputStream;
            if (section.Compress)
            {
                string str = base.Request.Headers["Accept-Encoding"];
                if (!string.IsNullOrEmpty(str))
                {
                    str = str.ToLowerInvariant();
                    if (str.Contains("gzip"))
                    {
                        response.AddHeader("Content-encoding", "gzip");
                        outputStream = new GZipStream(outputStream, CompressionMode.Compress);
                    }
                    else if (str.Contains("deflate"))
                    {
                        response.AddHeader("Content-encoding", "deflate");
                        outputStream = new DeflateStream(outputStream, CompressionMode.Compress);
                    }
                }
            }
            using (StreamWriter writer = new StreamWriter(outputStream))
            {
                foreach (FileInfoElement element2 in list)
                {
                    string cssContent = System.IO.File.ReadAllText(base.Server.MapPath(element2.Filename));
                    if (section.Compact)
                    {
                        string mimeType = element.MimeType;
                        if (mimeType != null)
                        {
                            if (!(mimeType == "text/css"))
                            {
                                if (((mimeType == "text/x-javascript") || (mimeType == "text/javascript")) || (mimeType == "text/ecmascript"))
                                {
                                    goto Label_026C;
                                }
                            }
                            else
                            {
                                cssContent = CSSMinify.Minify(base.Url, element2.Filename, base.Request.Url.AbsolutePath, cssContent);
                            }
                        }
                    }
                    goto Label_0278;
                Label_026C:
                    cssContent = JSMinify.Minify(cssContent);
                Label_0278:
                    writer.WriteLine(cssContent.Trim());
                }
            }
            if (section.CacheDuration > 0)
            {
                DateTime timestamp = base.HttpContext.Timestamp;
                HttpCachePolicyBase cache = response.Cache;
                int cacheDuration = section.CacheDuration;
                cache.SetCacheability(HttpCacheability.Public);
                cache.SetExpires(timestamp.AddSeconds((double) cacheDuration));
                cache.SetMaxAge(new TimeSpan(0, 0, cacheDuration));
                cache.SetValidUntilExpires(true);
                cache.SetLastModified(timestamp);
                cache.VaryByHeaders["Accept-Encoding"] = true;
                cache.VaryByParams["name"] = true;
                cache.VaryByParams["version"] = true;
                cache.VaryByParams["display"] = true;
                cache.VaryByParams["condition"] = true;
                cache.SetOmitVaryStar(true);
            }
        }

        [NonAction]
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("WebResource", "WebResource/{name}/{version}/{condition}", new { controller = "ExternalResource", action = "Index", condition = "" });
        }

        [NonAction]
        public static void RegisterRoutes(RouteCollection routes, string rootFolder)
        {
            routes.MapRoute("WebResource", rootFolder, new { controller = "WebResource", action = "Index" });
        }
    }
}

