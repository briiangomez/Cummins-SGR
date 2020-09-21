using Microsoft.AspNetCore.Http;

namespace SGR.Communicator.Models
{
    public class PagingParameters
    {
        public const string CurrentPageParameterName = "currentpage";
        public const string PageSizeParameterName = "pagesize";

        public static PagingParameters FromCurrentPageNPageSize(int currentPage, int pageSize)
        {
            return new PagingParameters { Type = PagingType.CurrentPageNPageSize, CurrentPage = currentPage, PageSize = pageSize, Count = pageSize, Start = (currentPage - 1) * pageSize };
        }

        public static PagingParameters FromStartNCount(int start, int count)
        {
            return new PagingParameters { Type = PagingType.StartNCount, Start = start, PageSize = count, Count = count, CurrentPage = (start / count) + 1 };
        }

        public static PagingParameters ParseRequest(Microsoft.AspNetCore.Http.HttpContext request, int? defaultPageSize = 10)
        {
            
            if ((request.Request.Query["start"] != "") || (request.Request.Query["count"] != ""))
            {
                int num = 0;
                if ((request.Request.Query["start"] != "") && !int.TryParse(request.Request.Query["start"], out num))
                {
                    num = 0;
                }
                int num2 = defaultPageSize.Value;
                if ((request.Request.Query["count"] != "") && !int.TryParse(request.Request.Query["count"], out num2))
                {
                    num2 = 10;
                }
                return FromStartNCount(num, num2);
            }
            int result = 1;
            if ((request.Request.Query["currentPage"] != "") && !int.TryParse(request.Request.Query["currentPage"], out result))
            {
                result = 1;
            }
            int num4 = defaultPageSize.Value;
            if ((request.Request.Query["pageSize"] != "") && !int.TryParse(request.Request.Query["pageSize"], out num4))
            {
                num4 = 10;
            }
            return FromCurrentPageNPageSize(result, num4);
        }

        public string ToQueryString()
        {
            if (this.Type == PagingType.StartNCount)
            {
                return string.Format("&start={0}&count={1}", this.Start, this.Count);
            }
            //return string.Format("&currentpage={0}&pagesize={1}", this.CurrentPage, this.PageSize);
            return string.Format("&currentpage={0}", this.CurrentPage, this.PageSize);
        }

        public string ToQueryString(int page)
        {
            if (this.Type == PagingType.StartNCount)
            {
                return string.Format("&start={0}&count={1}", (page - 1) * this.PageSize, this.Count);
            }
            //return string.Format("&currentpage={0}&pagesize={1}", page, this.PageSize);
            return string.Format("&currentpage={0}", page, this.PageSize);
        }

        public int Count { get; private set; }

        public int CurrentPage { get; private set; }

        public int PageSize { get; private set; }

        public int Start { get; private set; }

        public SGR.Communicator.Models.PagingType Type { get; set; }
    }
}

