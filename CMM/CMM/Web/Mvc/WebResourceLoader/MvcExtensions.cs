namespace CMM.Web.Mvc.WebResourceLoader
{
    using CMM.Web.Mvc;
    using CMM.Web.Mvc.WebResourceLoader.Configuration;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class MvcExtensions
    {
        private static string CreateAttributeList(RouteValueDictionary attributes)
        {
            StringBuilder builder = new StringBuilder();
            if (attributes != null)
            {
                foreach (string str in attributes.Keys)
                {
                    string str2 = attributes[str].ToString();
                    if (attributes[str] is bool)
                    {
                        str2 = str2.ToLowerInvariant();
                    }
                    builder.AppendFormat(" {0}=\"{1}\"", str.ToLowerInvariant().Replace("_", ""), str2);
                }
            }
            return builder.ToString();
        }

        public static IHtmlString ExternalResources(this HtmlHelper htmlHelper, string name)
        {
            return htmlHelper.ExternalResources(name, ((RouteValueDictionary) null));
        }

        public static IHtmlString ExternalResources(this HtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            return htmlHelper.ExternalResources(name, new RouteValueDictionary(htmlAttributes));
        }

        public static IHtmlString ExternalResources(this HtmlHelper htmlHelper, string name, RouteValueDictionary htmlAttributes)
        {
            ReferenceFomatter fomatter;
            ReferenceFomatter fomatter2 = null;
            ReferenceFomatter fomatter3 = null;
            ReferenceElement settings = GetSettings(CMM.Web.Mvc.AreaHelpers.GetAreaName(htmlHelper.ViewContext.RouteData), name);
            IDictionary<Condition, IList<FileInfoElement>> conditions = new Dictionary<Condition, IList<FileInfoElement>>(new ConditionComparer());
            foreach (FileInfoElement element in settings.Files)
            {
                Condition condition2 = new Condition {
                    If = element.If
                };
                Condition key = condition2;
                if (conditions.ContainsKey(key))
                {
                    conditions[key].Add(element);
                }
                else
                {
                    List<FileInfoElement> list = new List<FileInfoElement> {
                        element
                    };
                    conditions.Add(key, list);
                }
            }
            string attributes = CreateAttributeList(htmlAttributes);
            switch (settings.MimeType)
            {
                case "text/x-javascript":
                case "text/javascript":
                case "text/ecmascript":
                    if (fomatter2 == null)
                    {
                        fomatter2 = (filename, mimeType, attribs) => string.Format("<script src=\"{0}\" type=\"{1}\"{2}></script>", HttpUtility.HtmlAttributeEncode(filename), settings.MimeType, attribs);
                    }
                    fomatter = fomatter2;
                    return OutputReferences(htmlHelper.ViewContext, conditions, settings, attributes, fomatter);

                case "text/css":
                    if (fomatter3 == null)
                    {
                        fomatter3 = (filename, mimeType, attribs) => string.Format("<link rel=\"Stylesheet\" href=\"{0}\" type=\"{1}\"{2} />", HttpUtility.HtmlAttributeEncode(filename), settings.MimeType, attribs);
                    }
                    fomatter = fomatter3;
                    return OutputReferences(htmlHelper.ViewContext, conditions, settings, attributes, fomatter);
            }
            return new HtmlString(string.Empty);
        }

        public static IEnumerable<string> GetFiles(string areaName, string configName)
        {
            ReferenceElement settings = GetSettings(areaName, configName);
            IEnumerator enumerator = settings.Files.GetEnumerator();
            while (enumerator.MoveNext())
            {
                FileInfoElement current = (FileInfoElement) enumerator.Current;
                yield return current.Filename;
            }
        }

        private static ReferenceElement GetSettings(string areaName, string name)
        {
            ReferenceElement element = ConfigurationManager.GetSection(areaName).References[name];
            if (element == null)
            {
                throw new WebResourceException(string.Format("Web resource name {0} not found", name));
            }
            return element;
        }

        private static IHtmlString OutputReferences(ViewContext viewContext, IDictionary<Condition, IList<FileInfoElement>> conditions, ReferenceElement settings, string attributes, ReferenceFomatter formatter)
        {
            WebResourcesSection section = ConfigurationManager.GetSection(CMM.Web.Mvc.AreaHelpers.GetAreaName(viewContext.RouteData));
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<Condition, IList<FileInfoElement>> pair in conditions)
            {
                Condition key = pair.Key;
                IList<FileInfoElement> list = pair.Value;
                bool flag = !string.IsNullOrEmpty(key.If);
                if (flag)
                {
                    builder.AppendFormat("<!--[if {0}]>", key.If);
                    builder.AppendLine();
                }
                switch (section.Mode)
                {
                    case Mode.Debug:
                        foreach (FileInfoElement element in list)
                        {
                            builder.AppendLine(formatter(VirtualPathUtility.ToAbsolute(element.Filename), settings.MimeType, attributes));
                        }
                        break;

                    case Mode.Release:
                    {
                        string virtualPath = RouteTable.Routes.GetVirtualPath(viewContext.RequestContext, new RouteValueDictionary(new { controller = "WebResource", action = "Index", name = settings.Name, version = section.Version, condition = key.If })).VirtualPath;
                        builder.AppendLine(formatter(virtualPath, settings.MimeType, attributes));
                        break;
                    }
                }
                if (flag)
                {
                    builder.AppendFormat("<![endif]-->", new object[0]);
                    builder.AppendLine();
                }
            }
            return new HtmlString(builder.ToString());
        }

    }
}

