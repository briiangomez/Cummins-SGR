

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
namespace CMM.Survey.SurveyWriters
{
	public class PostWriter : ISurveyWriter
	{
		public const string BtnPreviousName = "__btnPrevious";
		public const string BtnNextName = "__btnNext";
		public const string BtnSubmitName = "__btnSubmit";
		private SurveyFormClass s0aEkT72q;
		private SurveyContext asjR9Bo1P;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private bool iera20Phe;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private bool ings97FBp;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string ufbW2gPqO;
		public bool CloseButton
		{
			
			get;
			
			set;
		}
		public bool ResetButton
		{
			
			get;
			
			set;
		}
		public string ResultUrl
		{
			
			get;
			
			set;
		}
		
		public void Initialize(SurveyFormClass survey)
		{
			this.s0aEkT72q = survey;
			this.asjR9Bo1P = survey.Context;
		}
		
		public void Write(System.IO.TextWriter writer)
		{
			this.Zejy4rWRw(writer);
		}
		
		public static int PageTurning(NameValueCollection form = null)
		{
			NameValueCollection nameValueCollection = form ?? HttpContext.Current.Request.Form;
			int result;
            if (!string.IsNullOrEmpty(nameValueCollection["__btnPrevious"]))
			{
				result = -1;
			}
			else
			{
                if (!string.IsNullOrEmpty(nameValueCollection["__btnNext"]))
				{
					result = 1;
				}
				else
				{
                    if (!string.IsNullOrEmpty(nameValueCollection["__btnSubmit"]))
					{
						result = 1;
					}
					else
					{
						result = 0;
					}
				}
			}
			return result;
		}
		
		public PostWriter()
		{
			
			
			this.CloseButton = true;
			this.ResetButton = false;
		}
		
		private void Zejy4rWRw(System.IO.TextWriter textWriter)
		{
			textWriter.WriteLine(string.Format("<form id=\"{0}\" name=\"{0}\" method=\"{1}\" action=\"{2}\" accept-charset=\"utf-8\">", this.asjR9Bo1P.FormId, this.asjR9Bo1P.FormMethod, this.asjR9Bo1P.FormAction));
			this.s0aEkT72q.FieldBox.Render(textWriter, new int?(this.s0aEkT72q.PageIndex));
			this.inge7CRv4(textWriter);
			this.wr6IGLmNk(textWriter);
            textWriter.WriteLine("</form>");
		}
		
		private void inge7CRv4(System.IO.TextWriter textWriter)
		{
            textWriter.HtmlComment("context parameters");
			textWriter.WriteLine("<div style=\"display:none;\">");
			foreach (System.Collections.Generic.KeyValuePair<string, object> current in this.asjR9Bo1P.Items)
			{
				textWriter.HtmlHidden(current.Key, current.Value);
			}
            textWriter.WriteLine("</div>");
		}
		
		private void wr6IGLmNk(System.IO.TextWriter textWriter)
		{
            textWriter.HtmlComment("form buttons");
			textWriter.WriteLine("<div class=\"runtime-btns\">");
			if (this.s0aEkT72q.PageIndexOverflow)
			{
				if (this.CloseButton)
				{
					textWriter.HtmlClose();
				}
				if (!string.IsNullOrEmpty(this.ResultUrl))
				{
                    textWriter.HtmlLinkSurvey(this.ResultUrl, "View Statistics");
				}
			}
			else
			{
				if (this.ResetButton)
				{
					textWriter.HtmlReset();
				}
				if (this.s0aEkT72q.PageIndex != 0)
				{
                    textWriter.HtmlSubmit("__btnPrevious", "< Previous");
				}
				if (this.s0aEkT72q.PageIndex == this.s0aEkT72q.PageCount - 1)
				{
                    textWriter.HtmlSubmit("__btnSubmit", "Submit >");
				}
				else
				{
                    textWriter.HtmlSubmit("__btnNext", "Next >");
				}
			}
            textWriter.WriteLine("</div>");			
		}
	}
}
