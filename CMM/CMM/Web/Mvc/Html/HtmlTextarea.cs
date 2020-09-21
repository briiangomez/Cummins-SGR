namespace CMM.Web.Mvc.Html
{
    using System;
    using System.Web.Mvc;

    public class HtmlTextarea : HtmlControlBase<HtmlTextarea>
    {
        private System.Web.Mvc.TagBuilder _TagBuilder = new System.Web.Mvc.TagBuilder("textarea");

        public override HtmlTextarea AddClass(string className)
        {
            this.TagBuilder.AddCssClass(className);
            return this;
        }

        public override HtmlTextarea Html(string html)
        {
            this.TagBuilder.InnerHtml = html;
            return this;
        }

        public override HtmlTextarea SetAttribute(string name, string value)
        {
            this.TagBuilder.MergeAttribute(name, value, true);
            return this;
        }

        public HtmlTextarea SetCols(int len)
        {
            this.TagBuilder.MergeAttribute("cols", len.ToString(), true);
            return this;
        }

        public override HtmlTextarea SetId(string id)
        {
            this.TagBuilder.MergeAttribute("id", id, true);
            base.Id = id;
            return this;
        }

        public override HtmlTextarea SetName(string name)
        {
            this.TagBuilder.MergeAttribute("name", name, true);
            return this;
        }

        public HtmlTextarea SetRows(int len)
        {
            this.TagBuilder.MergeAttribute("rows", len.ToString(), true);
            return this;
        }

        public HtmlTextarea SetValue(string value)
        {
            this.TagBuilder.MergeAttribute("value", value, true);
            return this;
        }

        public override HtmlTextarea Text(string text)
        {
            this.TagBuilder.SetInnerText(text);
            return this;
        }

        protected override System.Web.Mvc.TagBuilder TagBuilder
        {
            get
            {
                return this._TagBuilder;
            }
        }
    }
}

