namespace CMM.Web.Mvc.Grid
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    public interface IGridActionRouteValuesGetter
    {
        RouteValueDictionary GetValues(object dataItem, RouteValueDictionary routeValueDictionary, ViewContext viewContext);
    }
}

