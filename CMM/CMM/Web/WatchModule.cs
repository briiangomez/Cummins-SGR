namespace CMM.Web
{
    using System;
    using System.Diagnostics;
    using System.Web;

    public class WatchModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(this.OnBeginRequest);
            context.EndRequest += new EventHandler(this.OnEndRequest);
        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.IsLocal && HttpContext.Current.IsDebuggingEnabled)
            {
                string absolutePath = HttpContext.Current.Request.Url.AbsolutePath;
                if ((absolutePath.Length <= 4) || ((absolutePath[absolutePath.Length - 3] != '.') && (absolutePath[absolutePath.Length - 4] != '.')))
                {
                    Stopwatch stopwatch = new Stopwatch();
                    HttpContext.Current.Items["Stopwatch"] = stopwatch;
                    stopwatch.Start();
                }
            }
        }

        private void OnEndRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Items.Contains("Stopwatch") && (HttpContext.Current.Response.ContentType == "text/html"))
            {
                Stopwatch stopwatch = (Stopwatch) HttpContext.Current.Items["Stopwatch"];
                stopwatch.Stop();
                TimeSpan elapsed = stopwatch.Elapsed;
                string str = string.Format("{0}ms", elapsed.TotalMilliseconds);
                HttpContext.Current.Response.Write("<p>" + str + "</p>");
            }
        }
    }
}

