namespace CMM.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using CMM.Globalization;
    using System.Web.Routing;

    public class EmptySelectListDataSource : ISelectListDataSource
    {
        public IEnumerable<SelectListItem> GetSelectListItems(RequestContext requestContext, string filter)
        {
            return new SelectListItem[0];
        }
    }


}

