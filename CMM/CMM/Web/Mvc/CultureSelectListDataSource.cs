namespace CMM.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class CultureSelectListDataSource : ISelectListDataSource
    {
        public IEnumerable<SelectListItem> GetSelectListItems(RequestContext requestContext, string filter)
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo iteratorVariable1 in from c in cultures
                orderby c.DisplayName
                select c)
            {
                SelectListItem iteratorVariable2 = new SelectListItem {
                    Text = iteratorVariable1.DisplayName,
                    Value = iteratorVariable1.Name,
                    Selected = iteratorVariable1.Equals(Thread.CurrentThread.CurrentCulture)
                };
                yield return iteratorVariable2;
            }
        }

    }
}

