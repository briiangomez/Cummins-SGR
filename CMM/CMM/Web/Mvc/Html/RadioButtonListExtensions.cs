namespace CMM.Web.Mvc.Html
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class RadioButtonListExtensions
    {
        public static IHtmlString RadioButtonList(this HtmlHelper helper, string name, IEnumerable<string> items)
        {
            SelectList list = new SelectList(items);
            return helper.RadioButtonList(name, list);
        }

        public static IHtmlString RadioButtonList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> items)
        {
            TagBuilder builder = new TagBuilder("div");
            builder.AddCssClass("radio-list");
            foreach (SelectListItem item in items)
            {
                TagBuilder builder2 = new TagBuilder("p");
                builder2.AddCssClass("clearfix");
                string str = item.Value ?? item.Text;
                string str2 = name + "_" + str;
                MvcHtmlString str3 = helper.RadioButton(name, str, item.Selected, new { id = str2 });
                TagBuilder builder3 = new TagBuilder("label");
                builder3.MergeAttribute("for", str2);
                builder3.MergeAttribute("class", "radio-label");
                builder3.InnerHtml = item.Text ?? item.Value;
                builder2.InnerHtml = str3.ToString() + builder3.ToString();
                builder.InnerHtml = builder.InnerHtml + builder2.ToString();
            }
            return new HtmlString(builder.ToString());
        }
    }
}

