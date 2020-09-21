namespace CMM.Web.Mvc.Paging
{
    using System;
    using System.Runtime.CompilerServices;

    public class PagerOptions
    {
        private string _containerTagName;

        public PagerOptions()
        {
            this.AutoHide = true;
            this.PageIndexParameterName = "page";
            this.NumericPagerItemCount = 10;
            this.AlwaysShowFirstLastPageNumber = false;
            this.ShowPrevNext = true;
            this.PrevPageText = "<";
            this.NextPageText = ">";
            this.ShowNumericPagerItems = true;
            this.ShowFirstLast = false;
            this.FirstPageText = "First";
            this.LastPageText = "Last";
            this.ShowMorePagerItems = true;
            this.MorePageText = "...";
            this.ShowDisabledPagerItems = true;
            this.SeparatorHtml = "&nbsp;&nbsp;";
            this.UseJqueryAjax = false;
            this.ContainerTagName = "div";
            this.ShowPageIndexBox = false;
            this.ShowGoButton = true;
            this.PageIndexBoxType = CMM.Web.Mvc.Paging.PageIndexBoxType.TextBox;
            this.MaximumPageIndexItems = 80;
            this.GoButtonText = "Go";
            this.ContainerTagName = "div";
            this.InvalidPageIndexErrorMessage = "Invalid page index";
            this.PageIndexOutOfRangeErrorMessage = "Page index out of range";
            this.CurrentPagerItemWrapperFormatString = "<a class=\"current\">{0}</a>";
        }

        public bool AlwaysShowFirstLastPageNumber { get; set; }

        public bool AutoHide { get; set; }

        public string ContainerTagName
        {
            get
            {
                return this._containerTagName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("ContainerTagName can not be null or empty", "ContainerTagName");
                }
                this._containerTagName = value;
            }
        }

        public string CssClass { get; set; }

        public string CurrentPageNumberFormatString { get; set; }

        public string CurrentPagerItemWrapperFormatString { get; set; }

        public string FirstPageText { get; set; }

        public string GoButtonText { get; set; }

        public string GoToPageSectionWrapperFormatString { get; set; }

        public string HorizontalAlign { get; set; }

        public string Id { get; set; }

        public string InvalidPageIndexErrorMessage { get; set; }

        public string LastPageText { get; set; }

        public int MaximumPageIndexItems { get; set; }

        public string MorePagerItemWrapperFormatString { get; set; }

        public string MorePageText { get; set; }

        public string NavigationPagerItemWrapperFormatString { get; set; }

        public string NextPageText { get; set; }

        public int NumericPagerItemCount { get; set; }

        public string NumericPagerItemWrapperFormatString { get; set; }

        public CMM.Web.Mvc.Paging.PageIndexBoxType PageIndexBoxType { get; set; }

        public string PageIndexBoxWrapperFormatString { get; set; }

        public string PageIndexOutOfRangeErrorMessage { get; set; }

        public string PageIndexParameterName { get; set; }

        public string PageNumberFormatString { get; set; }

        public string PagerItemWrapperFormatString { get; set; }

        public string PrevPageText { get; set; }

        public string SeparatorHtml { get; set; }

        public bool ShowDisabledPagerItems { get; set; }

        public bool ShowFirstLast { get; set; }

        public bool ShowGoButton { get; set; }

        public bool ShowMorePagerItems { get; set; }

        public bool ShowNumericPagerItems { get; set; }

        public bool ShowPageIndexBox { get; set; }

        public bool ShowPrevNext { get; set; }

        internal bool UseJqueryAjax { get; set; }
    }
}

