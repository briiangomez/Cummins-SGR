using SGI.WebApp.ApiModels;
using System;
using System.Collections.Generic;

namespace SGI.WebApp.ViewModels
{
    public class PagedResultViewModel<V> where V : BaseApiModel
    {
        public List<V> Data { get; private set; }
        public PagingInfoViewModel Paging { get; private set; }
        public PagedResultViewModel(IEnumerable<V> items, int pageNo, int pageSize, int totalRecordCount)
        {
            Data = new List<V>(items);
            Paging = new PagingInfoViewModel
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalRecordCount = totalRecordCount,
                PageCount = totalRecordCount > 0 ? (int)Math.Ceiling(totalRecordCount / (double)pageSize) : 0
            };
        }

        public PagedResultViewModel()
        {
            Data = new List<V>();
            Paging = new PagingInfoViewModel();
        }
    }
}
