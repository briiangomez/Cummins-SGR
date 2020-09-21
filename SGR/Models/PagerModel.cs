namespace SGR.Communicator.Models
{
    using CMM.Web.Url;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web;

    public class PagerModel
    {
        private string _baseUrl;
        public const int PagingLength = 10;

        public PagerModel(int total, PagingParameters paging) : this(null, total, paging)
        {
        }

        public PagerModel(string baseUrl, int total, PagingParameters paging)
        {
            this.Total = total;
            this._baseUrl = baseUrl;
            this.Paging = paging;
            this.PageCount = Convert.ToInt32(Math.Ceiling((double) (((double) this.Total) / ((double) paging.PageSize))));
        }

        public string Url(int page)
        {
            string baseUrl = this.BaseUrl;
            if (baseUrl.IndexOf("?") < 0)
            {
                baseUrl = baseUrl + "?";
            }
            return (baseUrl + this.Paging.ToQueryString(page));
        }

        public string BaseUrl
        {
            get
            {
                if (this._baseUrl == null)
                {
                    //if (this.Paging.Type == PagingType.CurrentPageNPageSize)
                    //{
                    //    this._baseUrl = HttpContext.Current.Request.Url.ToString();
                    //    this._baseUrl = UrlUtility.RemoveQuery(this._baseUrl, new string[] { "currentPage" });
                    //    this._baseUrl = UrlUtility.RemoveQuery(this._baseUrl, new string[] { "pageSize" });
                    //}
                    //else
                    //{
                    //    this._baseUrl = HttpContext.Current.Request.Url.ToString();
                    //    this._baseUrl = UrlUtility.RemoveQuery(this._baseUrl, new string[] { "start" });
                    //    this._baseUrl = UrlUtility.RemoveQuery(this._baseUrl, new string[] { "count" });
                    //}
                }
                return this._baseUrl;
            }
        }

        public bool LeftEllipsis
        {
            get
            {
                return ((this.Paging.CurrentPage - 10) > 0);
            }
        }

        public bool LeftNavigatable
        {
            get
            {
                return (this.Paging.CurrentPage > 1);
            }
        }

        public string LeftNavigateUrl
        {
            get
            {
                return this.Url(this.Paging.CurrentPage - 1);
            }
        }

        public int LeftPage
        {
            get
            {
                return Math.Max(1, this.Paging.CurrentPage - 5);
            }
        }

        public int PageCount { get; private set; }

        public PagingParameters Paging { get; set; }

        public bool RightEllipsis
        {
            get
            {
                return ((this.Paging.CurrentPage + 10) <= this.PageCount);
            }
        }

        public bool RightNavigatable
        {
            get
            {
                return (this.Paging.CurrentPage < this.PageCount);
            }
        }

        public string RightNavigateUrl
        {
            get
            {
                return this.Url(this.Paging.CurrentPage + 1);
            }
        }

        public int RightPage
        {
            get
            {
                return Math.Min(this.Paging.CurrentPage + 5, this.PageCount);
            }
        }

        public int Total { get; private set; }
    }
}

