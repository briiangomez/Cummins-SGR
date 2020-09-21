namespace CMM.Web.Mvc.Constraints
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Routing;

    public class IgnoreConstraint : IRouteConstraint
    {
        private string[] _values;

        public IgnoreConstraint(params string[] ignoreValues)
        {
            this._values = ignoreValues;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string str = values[parameterName].ToString();
            return !this._values.Contains<string>(str, StringComparer.OrdinalIgnoreCase);
        }
    }
}

