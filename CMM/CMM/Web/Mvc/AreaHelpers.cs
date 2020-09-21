namespace CMM.Web.Mvc
{
    using CMM;
    using CMM.Web.Url;
    using System;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class AreaHelpers
    {
        public static string CombineAreaFilePhysicalPath(string areaName, params string[] paths)
        {
            return Path.Combine(Path.Combine(Settings.BaseDirectory, "Areas", areaName), Path.Combine(paths));
        }

        public static string CombineAreaFileVirtualPath(string areaName, params string[] paths)
        {
            string str = UrlUtility.Combine(new string[] { "~/", "Areas", areaName });
            return UrlUtility.Combine(new string[] { str, UrlUtility.Combine(paths) });
        }

        public static string GetAreaName(RouteBase route)
        {
            IRouteWithArea area = route as IRouteWithArea;
            if (area != null)
            {
                return area.Area;
            }
            Route route2 = route as Route;
            if ((route2 != null) && (route2.DataTokens != null))
            {
                return (route2.DataTokens["area"] as string);
            }
            return null;
        }

        public static string GetAreaName(RouteData routeData)
        {
            object obj2;
            if (routeData.DataTokens.TryGetValue("area", out obj2))
            {
                return (obj2 as string);
            }
            return GetAreaName(routeData.Route);
        }
    }
}

