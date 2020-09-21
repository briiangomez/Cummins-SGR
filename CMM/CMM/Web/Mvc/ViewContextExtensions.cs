namespace CMM.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public static class ViewContextExtensions
    {
        public static bool IsHandledBy<TController>(this ViewContext context)
        {
            return (context.Controller is TController);
        }
    }
}

