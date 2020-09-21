using HtmlAgilityPack;
using CMM.Survey.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
namespace CMM.Survey.SurveyFields
{
	public class RadioList : BaseQuestion
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass5
		{
			public string answerText;
			
			public c__DisplayClass5()
			{
				
				
			}
			
			public bool GetPostAnswerb__4(HtmlNode o)
			{
                return o.attr("value") == this.answerText;
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass10
		{
			public SurveyAnswer answerValue = new SurveyAnswer();
			
			public c__DisplayClass10()
			{
				
				
			}
			
			public bool SetAnswerb__b(HtmlNode o)
			{
				return o.identity() == this.answerValue.AnswerHtmlId;
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private static Func<SurveyAnswer, bool> s0aEkT72q;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private static Func<SurveyAnswer, bool> asjR9Bo1P;
		
		public RadioList(HtmlNode node) : base (node)
		{

		}
		
		private System.Collections.Generic.List<HtmlNode> Zejy4rWRw()
		{
			System.Collections.Generic.List<HtmlNode> list = new System.Collections.Generic.List<HtmlNode>();
            HtmlNode htmlNode = base.Node.ChildNodes.search("group_").First<HtmlNode>();
            System.Collections.Generic.List<HtmlNode> list2 = htmlNode.ChildNodes.search("item");
			foreach (HtmlNode current in list2)
			{
                list.Add(current.ChildNodes.first("input"));
			}
			return list;
		}
		
		public override System.Collections.Generic.List<SurveyAnswer> GetAnswerOptions()
		{
			System.Collections.Generic.List<SurveyAnswer> list = new System.Collections.Generic.List<SurveyAnswer>();
			System.Collections.Generic.List<HtmlNode> list2 = this.Zejy4rWRw();
			foreach (HtmlNode current in list2)
			{
				list.Add(new SurveyAnswer
				{
					AnswerType = SurveyAnswerType.Option, 
					AnswerHtmlId = current.identity(), 
					AnswerText = current.val()
				});
			}
			return list;
		}
		
		public override System.Collections.Generic.List<SurveyAnswer> GetPostAnswer()
		{
            c__DisplayClass5 c__DisplayClass = new c__DisplayClass5();

			System.Collections.Generic.List<SurveyAnswer> list;
			System.Collections.Generic.List<HtmlNode> source;
			NameValueCollection httpPost;
			
			list = new System.Collections.Generic.List<SurveyAnswer>();
			source = this.Zejy4rWRw();
			httpPost = base.GetHttpPost();
			if (httpPost.Count <= 0)
			{
				goto IL_11D;
			}
			string text = httpPost[base.CommentId];
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(new SurveyAnswer
				{
					AnswerType = SurveyAnswerType.Comment, 
					AnswerText = text, 
					AnswerHtmlId = base.CommentId
				});
				httpPost.Remove(base.CommentId);
			}
			
			c__DisplayClass.answerText = httpPost[base.FieldName];
			HtmlNode htmlNode = source.Where(new Func<HtmlNode, bool>(c__DisplayClass.GetPostAnswerb__4)).FirstOrDefault<HtmlNode>();
			if (htmlNode != null)
			{
				list.Add(new SurveyAnswer
				{
					AnswerType = SurveyAnswerType.Option, 
					AnswerHtmlId = htmlNode.identity(), 
					AnswerText = c__DisplayClass.answerText
				});
			}
			IL_11D:
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
			System.Collections.Generic.List<SurveyAnswer> list = new System.Collections.Generic.List<SurveyAnswer>();
			System.Collections.Generic.List<HtmlNode> list2 = this.Zejy4rWRw();
			foreach (HtmlNode current in list2)
			{
                if (current.hasAttr("checked"))
				{
					list.Add(new SurveyAnswer
					{
						AnswerType = SurveyAnswerType.Option, 
						AnswerHtmlId = current.identity(), 
						AnswerText = current.val()
					});
					break;
				}
			}
			string commentText = base.GetCommentText();
			if (!string.IsNullOrEmpty(commentText))
			{
				list.Add(new SurveyAnswer
				{
					AnswerType = SurveyAnswerType.Comment, 
					AnswerHtmlId = base.CommentId, 
					AnswerText = commentText
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
			RadioList.c__DisplayClass10 c__DisplayClass = new RadioList.c__DisplayClass10();
			RadioList.c__DisplayClass10 arg_50_0 = c__DisplayClass;
			if (RadioList.s0aEkT72q == null)
			{
				RadioList.s0aEkT72q = new Func<SurveyAnswer, bool>(RadioList.inge7CRv4);
			}
			arg_50_0.answerValue = value.Where(RadioList.s0aEkT72q).FirstOrDefault<SurveyAnswer>();
			if (c__DisplayClass.answerValue != null)
			{
				HtmlNode htmlNode = this.Zejy4rWRw().Where(new Func<HtmlNode, bool>(c__DisplayClass.SetAnswerb__b)).FirstOrDefault<HtmlNode>();
				if (htmlNode != null)
				{
                    htmlNode.attr("checked", "true");
				}
			}
			if (RadioList.asjR9Bo1P == null)
			{
				RadioList.asjR9Bo1P = new Func<SurveyAnswer, bool>(RadioList.wr6IGLmNk);
			}
			SurveyAnswer surveyAnswer = value.Where(RadioList.asjR9Bo1P).FirstOrDefault<SurveyAnswer>();
			if (surveyAnswer != null)
			{
				base.SetCommentText(surveyAnswer.AnswerText);
			}			
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		
		private static bool inge7CRv4(SurveyAnswer surveyAnswer)
		{
			return surveyAnswer.AnswerType == SurveyAnswerType.Option;
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		
		private static bool wr6IGLmNk(SurveyAnswer surveyAnswer)
		{
			return surveyAnswer.AnswerType == SurveyAnswerType.Comment;
		}
	}
}
