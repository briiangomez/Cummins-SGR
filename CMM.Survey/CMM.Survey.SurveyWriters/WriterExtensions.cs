using CMM.Globalization;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace CMM.Survey.SurveyWriters
{
	public static class WriterExtensions
	{
		
		public static System.IO.TextWriter Html(this System.IO.TextWriter writer, string markup)
		{
			writer.WriteLine(markup);
			return writer;
		}
		
		public static System.IO.TextWriter HtmlComment(this System.IO.TextWriter writer, string text)
		{
            return writer.Html(string.Format("<!-- {0} -->", text));
		}
		
		public static System.IO.TextWriter HtmlHidden(this System.IO.TextWriter writer, string name, object value)
		{
			return writer.Html(string.Format("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", name, value));
		}
		
		public static System.IO.TextWriter HtmlLink(this System.IO.TextWriter writer, string href, string text)
		{
			return writer.Html(string.Format("<a class=\"button\" href=\"{0}\">{1}</a>", href, text.Localize("")));
		}

        public static System.IO.TextWriter HtmlLinkSurvey(this System.IO.TextWriter writer, string href, string text)
        {
            return writer.Html(string.Format("<a class=\"button\" style=\"line-height: 20px\" href=\"{0}\">{1}</a>", href, text.Localize("")));
        }

        public static System.IO.TextWriter HtmlSubmit(this System.IO.TextWriter writer, string name, string value)
		{
			return writer.Html(string.Format("<input type=\"submit\" id=\"{0}\" name=\"{0}\" value=\"{1}\" class=\"button\" />", name, value.Localize("")));
		}
		
		public static System.IO.TextWriter HtmlReset(this System.IO.TextWriter writer)
		{
            return writer.Html(string.Format("<input type=\"reset\" id=\"btnReset\" value=\"{0}\" class=\"button\" />", "Reset".Localize("")));
		}
		
		public static System.IO.TextWriter HtmlClose(this System.IO.TextWriter writer)
		{
            return writer.Html(string.Format("<input type=\"button\" id=\"btnClose\" value=\"{0}\" class=\"button\" />", "Close".Localize("")));
		}
		
		public static System.IO.TextWriter HtmlScript(this System.IO.TextWriter writer, string jsText)
		{
            return writer.Html("<script type=\"text/javascript\">").Html(jsText).Html("</script>");
		}
		
		public static System.IO.TextWriter HtmlStyle(this System.IO.TextWriter writer, string cssText)
		{
            return writer.Html("<style type=\"text/css\">").Html(cssText).Html("</style>");
		}
	}
}
