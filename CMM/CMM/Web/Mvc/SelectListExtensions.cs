namespace CMM.Web.Mvc
{
    using CMM.Globalization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public static class SelectListExtensions
    {
        public static IEnumerable<SelectListItem> EmptyItem(this IEnumerable<SelectListItem> listItems, string emptyLabel)
        {
            SelectListItem[] first = new SelectListItem[1];
            SelectListItem item = new SelectListItem {
                Text = emptyLabel,
                Value = ""
            };
            first[0] = item;
            return first.Concat<SelectListItem>(listItems);
        }

        public static IEnumerable<SelectListItem> SetActiveItem(this IEnumerable<SelectListItem> listItems, object value)
        {
            if (value == null)
            {
                return listItems;
            }
            string[] values = null;
            if (value is IEnumerable<object>)
            {
                values = (from it in (IEnumerable<object>) value select it.ToString()).ToArray<string>();
            }
            else if (value is Enum)
            {
                values = new string[] { ((int) value).ToString() };
            }
            else
            {
                values = new string[] { value.ToString() };
            }
            return (from it in listItems select new SelectListItem { Text = it.Text.Localize(""), Value = it.Value, Selected = values.Contains<string>(it.Value, StringComparer.CurrentCultureIgnoreCase) });
        }
    }
}

