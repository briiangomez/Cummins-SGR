namespace CMM.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public static class JsonpResultContollerExtensions
    {
        public static JsonpResult Jsonp(this Controller controller, object data)
        {
            JsonpResult result = new JsonpResult {
                Data = data
            };
            result.ExecuteResult(controller.ControllerContext);
            return result;
        }
    }
}

