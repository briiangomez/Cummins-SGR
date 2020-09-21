namespace CMM.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Web.Routing;

    public static class RouteValuesHelpers
    {
        public static RouteValueDictionary GetRouteValues(RouteValueDictionary routeValues)
        {
            if (routeValues == null)
            {
                return new RouteValueDictionary();
            }
            return new RouteValueDictionary(routeValues);
        }

        public static RouteValueDictionary Merge(this RouteValueDictionary routeValues, string key, object value)
        {
            routeValues[key] = value;
            return routeValues;
        }

        public static RouteValueDictionary MergeRouteValues(string actionName, string controllerName, RouteValueDictionary implicitRouteValues, RouteValueDictionary routeValues, bool includeImplicitMvcValues)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary();
            if (includeImplicitMvcValues)
            {
                object obj2;
                if ((implicitRouteValues != null) && implicitRouteValues.TryGetValue("action", out obj2))
                {
                    dictionary["action"] = obj2;
                }
                if ((implicitRouteValues != null) && implicitRouteValues.TryGetValue("controller", out obj2))
                {
                    dictionary["controller"] = obj2;
                }
            }
            if (routeValues != null)
            {
                foreach (KeyValuePair<string, object> pair in GetRouteValues(routeValues))
                {
                    dictionary[pair.Key] = pair.Value;
                }
            }
            if (actionName != null)
            {
                dictionary["action"] = actionName;
            }
            if (controllerName != null)
            {
                dictionary["controller"] = controllerName;
            }
            return dictionary;
        }

        public static RouteValueDictionary ToRouteValues(this NameValueCollection values)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary();
            foreach (string str in values.AllKeys)
            {
                dictionary[str] = values[str];
            }
            return dictionary;
        }
    }
}

