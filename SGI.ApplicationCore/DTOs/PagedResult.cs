using SGI.ApplicationCore.Entities;
using System;
using System.Collections.Generic;

namespace SGI.ApplicationCore.DTOs
{
    public class PagedResult<T> where T : BaseEntity
    {
        public List<T> Data { get; set; }
        public PagingInfo Paging { get; private set; }
        public PagedResult(IEnumerable<T> items, int pageNo, int pageSize, int totalRecordCount)
        {
            Data = new List<T>(items);
            Paging = new PagingInfo
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalRecordCount = totalRecordCount,
                PageCount = totalRecordCount > 0 ? (int)Math.Ceiling(totalRecordCount / (double)pageSize) : 0
            };
        }

        public PagedResult()
        {
            Data = new List<T>();
            Paging = new PagingInfo();
        }
    }
}

