namespace CMM.Web.Mvc.Html
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IHtmlControl
    {
        IHtmlControl AddClass(string className);
        IHtmlControl Html(string html);
        IHtmlControl SetAttribute(string name, string value);
        IHtmlControl SetId(string id);
        IHtmlControl SetName(string name);
        IHtmlControl Text(string text);
        MvcHtmlString ToHtmlString();

        IDictionary<string, string> Attributes { get; }

        string Id { get; }

        string Name { get; }
    }
}

