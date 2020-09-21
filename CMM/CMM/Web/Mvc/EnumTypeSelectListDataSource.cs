namespace CMM.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class EnumTypeSelectListDataSource : ISelectListDataSource
    {
        public EnumTypeSelectListDataSource(Type enumType)
        {
            this.EnumType = enumType;
        }

        public IEnumerable<SelectListItem> GetSelectListItems(RequestContext requestContext, string filter)
        {
            IEnumerator enumerator = Enum.GetValues(this.EnumType).GetEnumerator();
            while (enumerator.MoveNext())
            {
                object current = enumerator.Current;
                SelectListItem iteratorVariable1 = new SelectListItem {
                    Text = current.ToString(),
                    Value = ((int) current).ToString()
                };
                yield return iteratorVariable1;
            }
        }

        public Type EnumType { get; private set; }

    }
}

