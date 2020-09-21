namespace CMM.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class SelectListItemTree : SelectListItem
    {
        public IEnumerable<SelectListItemTree> Items { get; set; }
    }
}

