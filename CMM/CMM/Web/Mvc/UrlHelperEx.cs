namespace CMM.Web.Mvc
{
    using System;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public static class UrlHelperEx
    {
        public static string Action<TController>(this UrlHelper helper, Expression<Func<TController, ActionResult>> func) where TController: Controller
        {
            return new UrlHelperWrapper(helper).Action<TController>(func);
        }
    }
}

