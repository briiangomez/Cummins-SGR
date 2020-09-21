namespace CMM.Web.Mvc.Paging
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PagedList<T> : IEnumerable<T>, IEnumerable
    {
        private IEnumerable<T> list;

        public PagedList(IList<T> items, int pageIndex, int pageSize)
        {
            this.PageSize = pageSize;
            this.TotalItemCount = items.Count;
            this.TotalPageCount = (int) Math.Ceiling((double) (((double) this.TotalItemCount) / ((double) this.PageSize)));
            this.CurrentPageIndex = pageIndex;
            this.StartRecordIndex = ((this.CurrentPageIndex - 1) * this.PageSize) + 1;
            this.EndRecordIndex = (this.TotalItemCount > (pageIndex * pageSize)) ? (pageIndex * pageSize) : this.TotalItemCount;
            this.list = items.Skip<T>(this.StartRecordIndex).Take<T>(pageSize);
        }

        public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount)
        {
            this.list = items;
            this.TotalItemCount = totalItemCount;
            this.TotalPageCount = (int) Math.Ceiling((double) (((double) totalItemCount) / ((double) pageSize)));
            this.CurrentPageIndex = pageIndex;
            this.PageSize = pageSize;
            this.StartRecordIndex = ((pageIndex - 1) * pageSize) + 1;
            this.EndRecordIndex = (this.TotalItemCount > (pageIndex * pageSize)) ? (pageIndex * pageSize) : totalItemCount;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public int CurrentPageIndex
        {
            get;
            set;
        }

        public int EndRecordIndex
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public int StartRecordIndex
        {
            get;
            set;
        }

        public int TotalItemCount
        {
            get;
            set;
        }

        public int TotalPageCount
        {
            get;
            set;
        }
    }
}

