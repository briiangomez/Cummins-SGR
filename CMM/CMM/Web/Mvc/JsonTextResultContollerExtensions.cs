namespace CMM.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public static class JsonTextResultContollerExtensions
    {
        public static JsonTextResult JsonText(this Controller controller, object data)
        {
            JsonTextResult result = new JsonTextResult(data);
            result.ExecuteResult(controller.ControllerContext);
            return result;
        }
    }
}

