

using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
namespace CMM.Survey.SurveyWriters
{
	public class GetWriter : ISurveyWriter
	{
		private SurveyFormClass ingIJwsh9;
		private SurveyContext ingYkkYlK;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private bool RJtQ6vSAa;
		public bool CloseButton
		{
			
			get;
			
			set;
		}
		
		public void Initialize(SurveyFormClass survey)
		{
			this.ingIJwsh9 = survey;
			this.ingYkkYlK = survey.Context;
		}
		
		public void Write(System.IO.TextWriter writer)
		{
			this.ingyIR1EL(writer);
		}
		
		public GetWriter()
		{
			
			
			this.CloseButton = true;
		}
		
		private void ingyIR1EL(System.IO.TextWriter textWriter)
		{
			this.ingIJwsh9.FieldBox.Render(textWriter, new int?(this.ingIJwsh9.PageIndex));
			this.O7defpKWU(textWriter);
		}
		
		private void O7defpKWU(System.IO.TextWriter textWriter)
		{
            textWriter.HtmlComment("form buttons");
			textWriter.WriteLine("<div class=\"runtime-btns\">");
			if (this.ingIJwsh9.PageIndexOverflow)
			{
				if (this.CloseButton)
				{
					textWriter.HtmlClose();
				}
			}
			else
			{
				int num;
				if (this.ingIJwsh9.PageIndex > 0)
				{
					NameValueCollection arg_B2_0 = this.ingYkkYlK.QueryString;
                    string arg_B2_1 = "PageIndex";
					num = this.ingYkkYlK.PageIndex - 1;
					arg_B2_0[arg_B2_1] = num.ToString();
                    textWriter.HtmlLink(this.ingYkkYlK.FormAction, "< Previous");
				}
				NameValueCollection arg_FF_0 = this.ingYkkYlK.QueryString;
                string arg_FF_1 = "PageIndex";
				num = this.ingYkkYlK.PageIndex + 1;
				arg_FF_0[arg_FF_1] = num.ToString();
                textWriter.HtmlLink(this.ingYkkYlK.FormAction, "Next >");
			}
            textWriter.WriteLine("</div>");			
		}
	}
}
