namespace CMM.Web.Mvc.Html
{
    using System;
    using System.Web.Mvc;

    public class HtmlTextbox : HtmlControlBase<HtmlTextbox>
    {
        private System.Web.Mvc.TagBuilder _TagBuilder = new System.Web.Mvc.TagBuilder("input");

        public HtmlTextbox()
        {
            this._TagBuilder.Attributes.Add("type", "text");
        }

        public override HtmlTextbox AddClass(string className)
        {
            this.TagBuilder.AddCssClass(className);
            return this;
        }

        public override HtmlTextbox Html(string html)
        {
            this.TagBuilder.InnerHtml = html;
            return this;
        }

        public override HtmlTextbox SetAttribute(string name, string value)
        {
            this.TagBuilder.MergeAttribute(name, value, true);
            return this;
        }

        public override HtmlTextbox SetId(string id)
        {
            this.TagBuilder.MergeAttribute("id", id, true);
            base.Id = id;
            return this;
        }

        public override HtmlTextbox SetName(string name)
        {
            this.TagBuilder.MergeAttribute("name", name, true);
            return this;
        }

        public HtmlTextbox SetValue(string value)
        {
            this.TagBuilder.MergeAttribute("value", value, true);
            return this;
        }

        public override HtmlTextbox Text(string text)
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

