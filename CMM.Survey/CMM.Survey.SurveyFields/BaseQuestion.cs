

using HtmlAgilityPack;
using CMM.Survey.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
namespace CMM.Survey.SurveyFields
{
	public abstract class BaseQuestion : BaseField, ISurveyQuestion, ISurveyField
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass8
		{
			public NameValueCollection postData;
			public NameValueCollection result;
			public BaseQuestion __this;
			
			public c__DisplayClass8()
			{
				
				
			}
			
			public void GetHttpPostb__6(string key)
			{
				this.result.Add(key, this.postData[key]);
			}
		}
		private string gOMA2Do5u;
		private int? ckWRx5Jmg;
		private bool? wex7jJGBa;
		private bool? RcKsnDfL6;
		private string DVpVQRFDI;
		public static string MultiValueSeparator;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string I5HBuSmJx;
		public string FieldTitle
		{
			
			get
			{
				if (this.gOMA2Do5u == null)
				{
                    HtmlNode htmlNode = base.Node.ChildNodes.search("fieldtitle").FirstOrDefault<HtmlNode>();
					if (htmlNode != null)
					{
						this.gOMA2Do5u = htmlNode.InnerText;
					}
				}
				return this.gOMA2Do5u ?? string.Empty;
			}
		}
		public int Numbering
		{
			
			get
			{
				if (!this.ckWRx5Jmg.HasValue)
				{
                    HtmlNode htmlNode = base.Node.ChildNodes.search("fieldindex").FirstOrDefault<HtmlNode>();
					if (htmlNode != null)
					{
                        this.ckWRx5Jmg = new int?(int.Parse(htmlNode.InnerText.Replace(".", string.Empty)));
					}
				}
				return this.ckWRx5Jmg.HasValue ? this.ckWRx5Jmg.Value : -1;
			}
		}
		public bool IsRequired
		{
			
			get
			{
				if (!this.wex7jJGBa.HasValue)
				{
                    HtmlNode htmlNode = base.Node.ChildNodes.search("require").FirstOrDefault<HtmlNode>();
					if (htmlNode != null)
					{
						this.wex7jJGBa = new bool?(!htmlNode.invisible(true));
					}
				}
				return this.wex7jJGBa.HasValue && this.wex7jJGBa.Value;
			}
		}
		public bool AllowComment
		{
			
			get
			{
				if (!this.RcKsnDfL6.HasValue)
				{
					HtmlNode htmlNode = this.wRsyirdC7();
					if (htmlNode != null)
					{
						this.RcKsnDfL6 = new bool?(!htmlNode.invisible(true));
					}
				}
				return this.RcKsnDfL6.HasValue && this.RcKsnDfL6.Value;
			}
		}
		public string GetGuideline
		{
			
			get
			{
				if (this.DVpVQRFDI == null)
				{
                    HtmlNode htmlNode = base.Node.ChildNodes.search("guideline").FirstOrDefault<HtmlNode>();
					if (htmlNode != null)
					{
						this.DVpVQRFDI = htmlNode.InnerHtml;
					}
				}
				return this.DVpVQRFDI ?? string.Empty;
			}
		}
		protected string CommentId
		{
			
			get
			{
                return this.FieldName + "_comment";
			}
		}
		public string FieldName
		{
			
			get;
			
			private set;
		}
		
		public BaseQuestion(HtmlNode node) : base (node)
		{
			this.FieldName = node.attr("fieldName");
		}
		
		private HtmlNode wRsyirdC7()
		{
            return base.Node.ChildNodes.search("comment").FirstOrDefault<HtmlNode>();
		}
		
		protected string GetCommentText()
		{
			HtmlNode htmlNode = this.wRsyirdC7();
			string result;
			if (htmlNode == null)
			{
				result = null;
			}
			else
			{
                HtmlNode node = htmlNode.ChildNodes.first("TEXTAREA");
				result = node.val();
			}
			return result;
		}
		
		protected void SetCommentText(string commentText)
		{
			HtmlNode htmlNode = this.wRsyirdC7();
			if (htmlNode != null)
			{
                HtmlNode node = htmlNode.ChildNodes.first("TEXTAREA");
				node.val(commentText);
			}
		}
		
		public virtual SurveyQuestion GetQuestion()
		{
			return new SurveyQuestion
			{
				QuestionText = this.FieldTitle, 
				QuestionIndex = this.Numbering - 1, 
				QuestionHtmlId = this.FieldName, 
				IsRequired = this.IsRequired, 
				AllowComment = this.AllowComment
			};
		}
		public abstract void SetAnswer(System.Collections.Generic.List<SurveyAnswer> value);
		public abstract System.Collections.Generic.List<SurveyAnswer> GetAnswer();
		public abstract System.Collections.Generic.List<SurveyAnswer> GetPostAnswer();
		public abstract System.Collections.Generic.List<SurveyAnswer> GetAnswerOptions();

        protected NameValueCollection GetHttpPost()
        {
            NameValueCollection postData = new NameValueCollection();
            string[] strArray = HttpContext.Current.Request.Form.ToString().Split(new char[] { '&' });
            foreach (string str2 in strArray)
            {
                string[] strArray2 = str2.Split(new char[] { '=' });
                string str3 = string.Empty;
                string str4 = string.Empty;
                if (strArray2.Length == 1)
                {
                    str3 = HttpUtility.UrlDecode(strArray2[0]);
                    str4 = string.Empty;
                }
                else if (strArray2.Length == 2)
                {
                    str3 = HttpUtility.UrlDecode(strArray2[0]);
                    str4 = HttpUtility.UrlDecode(strArray2[1]);
                }
                if (string.IsNullOrEmpty(postData[str3]))
                {
                    postData[str3] = str4;
                }
                else
                {
                    postData[str3] = postData[str3] + MultiValueSeparator + str4;
                }
            }
            NameValueCollection result = new NameValueCollection();
            postData.AllKeys.Where<string>(new Func<string, bool>(this.ingeV6FrT)).ToList<string>().ForEach(delegate(string key)
            {
                result.Add(key, postData[key]);
            });
            return result;
        }

				
		public override void Render(System.IO.TextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.FieldName))
			{
                writer.WriteLine(string.Format("<!-- {0} -->", this.FieldName));
			}
			base.Render(writer);
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		
		private bool ingeV6FrT(string text)
		{
			return text.StartsWith(this.FieldName);
		}
		
		static BaseQuestion()
		{
			// Note: this type is marked as 'beforefieldinit'.
			
			BaseQuestion.MultiValueSeparator = System.Guid.NewGuid().ToString();
		}
	}
}
