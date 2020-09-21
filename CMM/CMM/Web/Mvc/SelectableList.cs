namespace CMM.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class SelectableList : IEnumerable<SelectListItem>, IEnumerable
    {
        public SelectableList(IEnumerable<SelectListItem> items)
        {
            this.Items = items;
        }

        public static SelectableList Build<T>(IEnumerable<T> items, Func<T, object> dataValueField, Func<T, object> dataTextField, params T[] selecteds)
        {
            Func<T, bool> predicate = null;
            Func<T, object> selector = null;
            List<SelectListItem> list = new List<SelectListItem>();
            List<object> source = new List<object>();
            if ((selecteds != null) && (selecteds.Length > 0))
            {
                if (predicate == null)
                {
                    predicate = i => i != null;
                }
                if (selector == null)
                {
                    selector = i => dataValueField(i);
                }
                source = selecteds.Where<T>(predicate).Select<T, object>(selector).ToList<object>();
            }
            foreach (T local in items)
            {
                object value = dataValueField(local);
                object obj2 = dataTextField(local);
                SelectListItem item2 = new SelectListItem {
                    Value = value.ToString(),
                    Text = obj2.ToString()
                };
                SelectListItem item = item2;
                item.Selected = source.Any<object>(m => m.Equals(value));
                list.Add(item);
            }
            return new SelectableList(list);
        }

        public IEnumerator GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        IEnumerator<SelectListItem> IEnumerable<SelectListItem>.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        private IEnumerable<SelectListItem> Items { get; set; }
    }
}

