

using HtmlAgilityPack;
using CMM.Survey.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
namespace CMM.Survey.SurveyFields
{
	public class TextArea : BaseQuestion
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private static Func<SurveyAnswer, bool> x2PI0DgiM;
		
		public TextArea(HtmlNode node) : base(node)
		{

		}
		
		private HtmlNode g15ynZap8()
		{
            HtmlNode htmlNode = base.Node.ChildNodes.search("wrap").First<HtmlNode>();
            return (htmlNode == null) ? null : htmlNode.ChildNodes.first("textarea");
		}
		
		public override System.Collections.Generic.List<SurveyAnswer> GetAnswerOptions()
		{
			return null;
		}
		
		public override System.Collections.Generic.List<SurveyAnswer> GetPostAnswer()
		{
			System.Collections.Generic.List<SurveyAnswer> list;
			HtmlNode node;
			string text;
			bool flag;
			
			list = new System.Collections.Generic.List<SurveyAnswer>();
			node = this.g15ynZap8();
			NameValueCollection httpPost = base.GetHttpPost();
			if (httpPost.Count <= 0)
			{
				goto IL_95;
			}
			text = httpPost[base.FieldName];
			flag = string.IsNullOrEmpty(text);
			
			if (!flag)
			{
				list.Add(new SurveyAnswer
				{
					AnswerType = SurveyAnswerType.Input, 
					AnswerHtmlId = node.identity(), 
					AnswerText = text
				});
			}
			IL_95:
			if (list.Count == 0)
			{
				list.Add(new SurveyAnswer
				{
					AnswerType = SurveyAnswerType.Empty
				});
			}
			return list;
		}
		
		public override System.Collections.Generic.List<SurveyAnswer> GetAnswer()
		{
			System.Collections.Generic.List<SurveyAnswer> list;
			
			list = new System.Collections.Generic.List<SurveyAnswer>();
			HtmlNode node = this.g15ynZap8();
			string text = node.val();
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(new SurveyAnswer
				{
					AnswerType = SurveyAnswerType.Input, 
					AnswerHtmlId = node.identity(), 
					AnswerText = text
				});
			}
			
			if (list.Count == 0)
			{
				list.Add(new SurveyAnswer
				{
					AnswerType = SurveyAnswerType.Empty
				});
			}
			return list;
		}
		
		public override void SetAnswer(System.Collections.Generic.List<SurveyAnswer> value)
		{
			if (TextArea.x2PI0DgiM == null)
			{
				TextArea.x2PI0DgiM = new Func<SurveyAnswer, bool>(TextArea.LkxeOZVkQ);
			}
			SurveyAnswer surveyAnswer = value.Where(TextArea.x2PI0DgiM).FirstOrDefault<SurveyAnswer>();
			if (surveyAnswer != null)
			{
				this.g15ynZap8().val(surveyAnswer.AnswerText);
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		
		private static bool LkxeOZVkQ(SurveyAnswer surveyAnswer)
		{
			return surveyAnswer.AnswerType == SurveyAnswerType.Input;
		}
	}
}
