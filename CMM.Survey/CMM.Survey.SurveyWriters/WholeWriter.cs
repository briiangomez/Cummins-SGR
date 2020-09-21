using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
namespace CMM.Survey.SurveyWriters
{
	public class WholeWriter : ISurveyWriter
	{
		private SurveyFormClass Wbi62Z7wI;
		private SurveyContext Nl5HiPlgD;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private System.Collections.Generic.List<System.IO.FileInfo> qCXXxmTCP;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private System.Collections.Generic.List<System.IO.FileInfo> c06SUnQ3L;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string Civ1DnNfP;
		public System.Collections.Generic.List<System.IO.FileInfo> StyleResources
		{
			
			get;
			
			private set;
		}
		public System.Collections.Generic.List<System.IO.FileInfo> ScriptResources
		{
			
			get;
			
			private set;
		}
		public string VirtualPath
		{
			
			get;
			
			set;
		}
		
		public void Initialize(SurveyFormClass survey)
		{
			this.Wbi62Z7wI = survey;
			this.Nl5HiPlgD = survey.Context;
		}
		
		public void Write(System.IO.TextWriter writer)
		{
			this.Zejy4rWRw(writer);
		}
		
		public WholeWriter()
		{	
			this.StyleResources = new System.Collections.Generic.List<System.IO.FileInfo>();
			this.ScriptResources = new System.Collections.Generic.List<System.IO.FileInfo>();
		}
		
		private void Zejy4rWRw(System.IO.TextWriter textWriter)
		{
            textWriter.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            textWriter.WriteLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            textWriter.WriteLine("<head>");
            this.inge7CRv4(textWriter);
            this.wr6IGLmNk(textWriter);
            this.s0aEkT72q(textWriter);
            textWriter.WriteLine("</head>");
            textWriter.WriteLine("<body>");
            textWriter.WriteLine("<div class=\"runtime-body cusbody\">");
            textWriter.WriteLine(string.Format("<form method=\"{0}\" action=\"{1}\" accept-charset=\"utf-8\">", this.Nl5HiPlgD.FormMethod, this.Nl5HiPlgD.FormAction));
            this.asjR9Bo1P(textWriter);
            textWriter.WriteLine("</form>");
            textWriter.WriteLine("</div>");
            textWriter.WriteLine("</body>");
            textWriter.WriteLine("</html>");

		}
		
		private void inge7CRv4(System.IO.TextWriter textWriter)
		{
			string formTitle = this.Nl5HiPlgD.Handler.GetFormTitle();
            textWriter.WriteLine("<title>" + formTitle + "</title>");
		}
		
		private void wr6IGLmNk(System.IO.TextWriter writer)
		{
			foreach (System.IO.FileInfo current in this.StyleResources)
			{
				if (current.Exists)
				{
					string text = System.IO.File.ReadAllText(current.FullName);
                    string relativeAction = string.IsNullOrEmpty(this.VirtualPath) ? "/" : this.VirtualPath;
                    string text2 = current.DirectoryName.Replace(HttpContext.Current.Server.MapPath("~"), string.Empty);
                    text2 = RuntimeKit.AbsoluteAction(relativeAction) + text2.Replace(@"\", "/").TrimStart(new char[]
					{
						'/'
					});
                    text = text.Replace("url(../images/", string.Format("url({0}/images/", text2));
					writer.HtmlStyle(text.Trim(System.Environment.NewLine.ToCharArray()));
				}
			}			
		}
		
		private void s0aEkT72q(System.IO.TextWriter writer)
		{
			foreach (System.IO.FileInfo current in this.ScriptResources)
			{
				if (current.Exists)
				{
					string text = System.IO.File.ReadAllText(current.FullName);
					writer.HtmlScript(text.Trim(System.Environment.NewLine.ToCharArray()));
				}
			}
		}
		
		private void asjR9Bo1P(System.IO.TextWriter textWriter)
		{
			this.Wbi62Z7wI.FieldBox.Render(textWriter, null);
			this.iera20Phe(textWriter);
			this.ings97FBp(textWriter);
		}
		
		private void iera20Phe(System.IO.TextWriter textWriter)
		{
            textWriter.HtmlComment("context parameters");
            textWriter.WriteLine("<div style=\"display:none;\">");
            this.Nl5HiPlgD.Items["__pageIndex"] = this.Wbi62Z7wI.PageCount;
            foreach (KeyValuePair<string, object> pair in this.Nl5HiPlgD.Items)
            {
                textWriter.HtmlHidden(pair.Key, pair.Value);
            }
            textWriter.WriteLine("</div>");

		}
		
		private void ings97FBp(System.IO.TextWriter textWriter)
		{
            textWriter.HtmlComment("form buttons");
            textWriter.WriteLine("<div class=\"runtime-btns\">");
            textWriter.HtmlSubmit("__btnSubmit", "Submit");
            textWriter.WriteLine("</div>");

		}
	}
}
