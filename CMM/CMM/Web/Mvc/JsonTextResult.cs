namespace CMM.Web.Mvc
{
    using System;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    public class JsonTextResult : ContentResult
    {
        public JsonTextResult(object data)
        {
            base.ContentType = "text/plain";
            base.Content = new JavaScriptSerializer().Serialize(data);
        }
    }
}

