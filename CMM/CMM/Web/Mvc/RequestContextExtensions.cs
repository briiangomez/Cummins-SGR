namespace CMM.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.Routing;

    public static class RequestContextExtensions
    {
        public static RouteValueDictionary AllRouteValues(this RequestContext requestContext)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary(requestContext.RouteData.Values);
            foreach (string str in requestContext.HttpContext.Request.QueryString.Keys)
            {
                dictionary[str] = requestContext.HttpContext.Request.QueryString[str];
            }
            return dictionary;
        }

        public static string GetRequestValue(this RequestContext requestContext, string name)
        {
            if (requestContext.RouteData.Values[name] != null)
            {
                return requestContext.RouteData.Values[name].ToString();
            }
            return requestContext.HttpContext.Request[name];
        }
    }
}

