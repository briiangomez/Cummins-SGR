namespace CMM.Web.Mvc.Routing
{
    using CMM.IO;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteTableRegister
    {
        private static RouteValueDictionary GetConstraints(RouteConfigElement route)
        {
            return GetRouteValueDictionary(route.Constraints.Attributes);
        }

        private static RouteValueDictionary GetDataTokens(RouteConfigElement route)
        {
            return GetRouteValueDictionary(route.DataTokens.Attributes);
        }

        private static RouteValueDictionary GetDefaults(RouteConfigElement route)
        {
            return GetRouteValueDictionary(route.Defaults.Attributes);
        }

        private static IRouteHandler GetInstanceOfRouteHandler(RouteConfigElement route)
        {
            IRouteHandler handler;
            if (string.IsNullOrEmpty(route.RouteHandlerType))
            {
                return new MvcRouteHandler();
            }
            try
            {
                handler = Activator.CreateInstance(Type.GetType(route.RouteHandlerType)) as IRouteHandler;
            }
            catch (Exception exception)
            {
                throw new ApplicationException(string.Format("Can't create an instance of IRouteHandler {0}", route.RouteHandlerType), exception);
            }
            return handler;
        }

        private static RouteTableSection GetRouteTableSection(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                return (RouteTableSection) ConfigurationManager.GetSection("routeTable");
            }
            RouteTableSection section = (RouteTableSection) Activator.CreateInstance(typeof(RouteTableSection));
            section.DeserializeSection(IOUtility.ReadAsString(file));
            return section;
        }

        private static RouteValueDictionary GetRouteValueDictionary(Dictionary<string, string> attributes)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary();
            foreach (KeyValuePair<string, string> pair in attributes)
            {
                if (pair.Key == "Namespaces")
                {
                    dictionary.Add(pair.Key, pair.Value.Split(new char[] { ',' }));
                }
                else
                {
                    dictionary.Add(pair.Key, pair.Value);
                }
            }
            return dictionary;
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            RegisterRoutes(routes, string.Empty);
        }

        public static void RegisterRoutes(RouteCollection routes, string routeFile)
        {
            lock (routes)
            {
                RouteTableSection routeTableSection = GetRouteTableSection(routeFile);
                if (routeTableSection != null)
                {
                    if (routeTableSection.Ignores.Count > 0)
                    {
                        foreach (ConfigurationElement element in routeTableSection.Ignores)
                        {
                            IgnoreRouteInternal internal3 = new IgnoreRouteInternal(((IgnoreRouteElement) element).Url) {
                                Constraints = GetRouteValueDictionary(((IgnoreRouteElement) element).Constraints.Attributes)
                            };
                            IgnoreRouteInternal item = internal3;
                            routes.Add(item);
                        }
                    }
                    if (routeTableSection.Routes.Count > 0)
                    {
                        for (int i = 0; i < routeTableSection.Routes.Count; i++)
                        {
                            RouteConfigElement route = routeTableSection.Routes[i];
                            if (routes[route.Name] == null)
                            {
                                if (string.IsNullOrEmpty(route.RouteType))
                                {
                                    routes.Add(route.Name, new Route(route.Url, GetDefaults(route), GetConstraints(route), GetDataTokens(route), GetInstanceOfRouteHandler(route)));
                                }
                                else
                                {
                                    RouteBase base2 = (RouteBase) Activator.CreateInstance(Type.GetType(route.RouteType), new object[] { route.Url, GetDefaults(route), GetConstraints(route), GetDataTokens(route), GetInstanceOfRouteHandler(route) });
                                    routes.Add(route.Name, base2);
                                }
                            }
                        }
                    }
                }
            }
        }

        private sealed class IgnoreRouteInternal : Route
        {
            public IgnoreRouteInternal(string url) : base(url, new StopRoutingHandler())
            {
            }

            public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary routeValues)
            {
                return null;
            }
        }
    }
}

