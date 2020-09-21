namespace CMM.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class UrlResult : ActionResult
    {
        public UrlResult(string physicalPath)
        {
            this.PhysicalPath = physicalPath;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "text/plain";
            context.HttpContext.Response.Write(this.ResolveClientUrl(context));
        }

        private string ResolveClientUrl(ControllerContext context)
        {
            string str = context.HttpContext.Server.MapPath("~");
            return this.PhysicalPath.Substring(str.Length - 1, (this.PhysicalPath.Length - str.Length) + 1).Replace(@"\", "/");
        }

        public string PhysicalPath { get; set; }
    }
}

