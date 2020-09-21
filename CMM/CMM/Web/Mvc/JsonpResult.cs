namespace CMM.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    public class JsonpResult : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.JsonCallback = context.HttpContext.Request["jsoncallback"];
            if (string.IsNullOrEmpty(this.JsonCallback))
            {
                this.JsonCallback = context.HttpContext.Request["callback"];
            }
            if (string.IsNullOrEmpty(this.JsonCallback))
            {
                throw new ArgumentNullException("JsonCallback required for JSONP response.");
            }
            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            if (this.Data != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                response.Write(string.Format("{0}({1});", this.JsonCallback, serializer.Serialize(this.Data)));
            }
        }

        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public object Data { get; set; }

        public string JsonCallback { get; set; }
    }
}

