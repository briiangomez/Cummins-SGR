namespace SGR.Communicator.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web;

    public class ListModel<T>
    {
        private string _queryString;
        private bool _queryStringBuilded;

        public virtual string ToQueryString()
        {
            if (!this._queryStringBuilded)
            {
                this._queryString = ((this.Search == null) ? string.Empty : this.Search.ToQueryString()) + ((this.Pager == null) ? string.Empty : this.Pager.Paging.ToQueryString());
                this._queryStringBuilded = true;
            }
            return this._queryString;
        }

        public IEnumerable<T> Items { get; set; }

        public ListFormModel ListForm { get; set; }


        public PagerModel Pager
        {
            //get
            //{
            //    return (Microsoft.AspNetCore.Http.HttpContext.Current.Items["PagerModel"] as PagerModel);
            //}
            //set
            //{
            //    HttpContext.Current.Items["PagerModel"] = value;
            //}

            get; set;
        }

        public SearchModel Search { get; set; }

        public string Busqueda { get; set; }

        public string Orden { get; set; }
    }
}

