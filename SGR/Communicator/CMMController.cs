using System;
using System.Linq;
using CMM.Globalization;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace SGR.Communicator
{
    [CultureFilter]
    public class CMMController : Controller
    {
        private SGR.Communicator.Models.PagingParameters _pagingParameters;
        private int _total;

        protected static string Added(string item)
        {
            return string.Format("{0} has been added".Localize(""), item);
        }

        protected static string AddFailed(string item)
        {
            return string.Format("The attempt to add the {0} has failed.".Localize(""), item);
        }

        protected static string Deleted(string item)
        {
            return string.Format("{0} has been deleted".Localize(""), item);
        }

        protected static string DeleteFailed(string item)
        {
            return string.Format("The attempt to delete the {0} has failed.".Localize(""), item);
        }

        public void Error(string message, Exception exception = null, int? timeout = 20)
        {
            this.Message = ActionMessage.Error(message, exception);
            this.Message.Timeout = timeout;
            if (exception != null)
            {
                LogManager.GetLogger("Web").Error(message, exception);
            }
        }

        public void Info(string message, int? timeout = 5)
        {
            this.Message = ActionMessage.Info(message);
            this.Message.Timeout = timeout;
        }

        public void Menu(params string[] ids)
        {
            int i = 0;
            base.ViewData["Menu"] = (from o in ids select string.Join("_", ids, 0, ++i)).ToArray<string>();
        }

        public void MenuNull()
        {
            base.ViewData["Menu"] = null;
        }

        protected static string OperationFailed()
        {
            return "Operation failed".Localize("");
        }

        protected static string Saved(string item)
        {
            return string.Format("{0} has been saved".Localize(""), item);
        }

        protected static string SaveFailed(string item)
        {
            return string.Format("The attempt to save the {0} has failed".Localize(""), item);
        }

        public void SetPagingParameters(int defaultPageSize)
        {
            this._pagingParameters = SGR.Communicator.Models.PagingParameters.ParseRequest(this.HttpContext, new int?(defaultPageSize));
        }

        public void Warning(string message, Exception ex = null, int? timeout = 10)
        {
            this.Message = ActionMessage.Warning(message, ex);
            this.Message.Timeout = timeout;
        }

        public SGR.Communicator.CommunicatorContext CommunicatorContext
        {
            get
            {
                return SGR.Communicator.CommunicatorContext.Current;
            }
        }

        public ActionMessage Message
        {
            get
            {
                return (base.TempData["Message"] as ActionMessage);
            }
            set
            {
                base.TempData["Message"] = value;
            }
        }

        public Models.PagerModel Pager
        {
            get
            {
                return (base.HttpContext.Items["PagerModel"] as Models.PagerModel);
            }
            set
            {
                base.HttpContext.Items["PagerModel"] = value;
            }
        }

        public SGR.Communicator.Models.PagingParameters PagingParameters
        {
            get
            {
                if (this._pagingParameters == null)
                {
                    this._pagingParameters = SGR.Communicator.Models.PagingParameters.ParseRequest(this.HttpContext,  10);
                }
                return this._pagingParameters;
            }
        }

        public int Total
        {
            get
            {
                return this._total;
            }
            set
            {
                this._total = value;
                Models.PagerModel model = new Models.PagerModel(this._total, this.PagingParameters);
                if ((model.PageCount > 0) && (model.Paging.CurrentPage > model.PageCount))
                {
                    this._pagingParameters = SGR.Communicator.Models.PagingParameters.FromCurrentPageNPageSize(model.PageCount, this._pagingParameters.PageSize);
                    this._pagingParameters.Type = model.Paging.Type;
                    model = new Models.PagerModel(this._total, this._pagingParameters);
                }
                this.Pager = model;
            }
        }
    }
}

