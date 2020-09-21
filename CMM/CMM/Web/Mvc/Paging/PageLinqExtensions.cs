namespace CMM.Web.Mvc.Paging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class PageLinqExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> allItems, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            int count = (pageIndex - 1) * pageSize;
            IEnumerable<T> items = allItems.Skip<T>(count).Take<T>(pageSize);
            return new PagedList<T>(items, pageIndex, pageSize, allItems.Count<T>());
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> allItems, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            int count = (pageIndex - 1) * pageSize;
            IQueryable<T> items = allItems.Skip<T>(count).Take<T>(pageSize);
            return new PagedList<T>(items, pageIndex, pageSize, allItems.Count<T>());
        }
    }
}

