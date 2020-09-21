namespace CMM.Web.Mvc.Html
{
    using System;
    using System.Web.Mvc;

    public class HtmlHidden : HtmlControlBase<HtmlHidden>
    {
        private System.Web.Mvc.TagBuilder _TagBuilder = new System.Web.Mvc.TagBuilder("input");

        public HtmlHidden()
        {
            this._TagBuilder.Attributes.Add("type", "hidden");
        }

        public override HtmlHidden AddClass(string className)
        {
            this.TagBuilder.AddCssClass(className);
            return this;
        }

        public override HtmlHidden Html(string html)
        {
            this.TagBuilder.InnerHtml = html;
            return this;
        }

        public override HtmlHidden SetAttribute(string name, string value)
        {
            this.TagBuilder.MergeAttribute(name, value, true);
            return this;
        }

        public override HtmlHidden SetId(string id)
        {
            this.TagBuilder.MergeAttribute("id", id, true);
            base.Id = id;
            return this;
        }

        public override HtmlHidden SetName(string name)
        {
            this.TagBuilder.MergeAttribute("name", name, true);
            return this;
        }

        public HtmlHidden SetValue(string value)
        {
            this.TagBuilder.MergeAttribute("value", value, true);
            return this;
        }

        public override HtmlHidden Text(string text)
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

