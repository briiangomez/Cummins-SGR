namespace CMM
{
    using System;
    using System.Web;

    public class ContextualCompositionContainerHttpModule : IHttpModule
    {
        private void context_EndRequest(object sender, EventArgs e)
        {
            CallContext context = HttpContext.Current.Items[CallContext.CONTEXT_CONTAINER] as CallContext;
            foreach (object obj2 in HttpContext.Current.Items)
            {
                if (obj2 is IDisposable)
                {
                    (obj2 as IDisposable).Dispose();
                }
            }
            HttpContext.Current.Items.Clear();
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.EndRequest += new EventHandler(this.context_EndRequest);
        }
    }
}

