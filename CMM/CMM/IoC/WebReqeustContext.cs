namespace CMM.IoC
{
    using System;
    using System.Collections;
    using System.Web;

    internal class WebReqeustContext : IRequestContext
    {
        public IDictionary Items
        {
            get
            {
                return HttpContext.Current.Items;
            }
        }
    }
}

