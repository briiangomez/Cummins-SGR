namespace CMM.Web.Mvc
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.Compilation;
    using System.Web.Mvc;

    public static class ResourceHtmlHelper
    {
        private static string GetResourceString(HttpContextBase httpContext, string expression, string virtualPath, object[] args)
        {
            ExpressionBuilderContext context = new ExpressionBuilderContext(virtualPath);
            ResourceExpressionBuilder builder = new ResourceExpressionBuilder();
            ResourceExpressionFields fields = (ResourceExpressionFields) builder.ParseExpression(expression, typeof(string), context);
            if (!string.IsNullOrEmpty(fields.ClassKey))
            {
                return string.Format((string) httpContext.GetGlobalResourceObject(fields.ClassKey, fields.ResourceKey, CultureInfo.CurrentUICulture), args);
            }
            return string.Format((string) httpContext.GetLocalResourceObject(virtualPath, fields.ResourceKey, CultureInfo.CurrentUICulture), args);
        }

        private static string GetVirtualPath(HtmlHelper htmlhelper)
        {
            string name = htmlhelper.ViewDataContainer.GetType().Name;
            int length = name.LastIndexOf('_');
            return ("~/" + name.Substring(0, length).Replace('_', '/') + "." + name.Substring(length + 1));
        }

        public static string Resource(this Controller controller, string expression, params object[] args)
        {
            return GetResourceString(controller.HttpContext, expression, "~/", args);
        }

        public static string Resource(this HtmlHelper htmlhelper, string expression, params object[] args)
        {
            string virtualPath = GetVirtualPath(htmlhelper);
            return GetResourceString(htmlhelper.ViewContext.HttpContext, expression, virtualPath, args);
        }
    }
}

