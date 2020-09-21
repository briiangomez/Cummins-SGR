using CMM.Survey.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Specialized;
using System.Linq;

namespace CMM.Survey.SurveyFields
{
	public class TextLabel : BaseQuestion
	{
		
		private static Func<SurveyAnswer, bool> wr6IGLmNk;

        public TextLabel(HtmlNode node)
            : base(node)
		{
		}
		
		private HtmlNode Zejy4rWRw()
		{
            //HtmlNode htmlNode = base.Node.ChildNodes.search("wrap").First<HtmlNode>();
            //return (htmlNode == null) ? null : htmlNode.ChildNodes.first("input");
		    return null;
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
			node = this.Zejy4rWRw();
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
			HtmlNode node = this.Zejy4rWRw();
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
			if (TextLabel.wr6IGLmNk == null)
			{
				TextLabel.wr6IGLmNk = new Func<SurveyAnswer, bool>(TextLabel.inge7CRv4);
			}
			SurveyAnswer surveyAnswer = value.Where(TextLabel.wr6IGLmNk).FirstOrDefault<SurveyAnswer>();
			if (surveyAnswer != null)
			{
				this.Zejy4rWRw().val(surveyAnswer.AnswerText);
			}
		}
		
		
		private static bool inge7CRv4(SurveyAnswer surveyAnswer)
		{
			return surveyAnswer.AnswerType == SurveyAnswerType.Input;
		}
	}
}
