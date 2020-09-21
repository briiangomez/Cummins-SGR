

using HtmlAgilityPack;
using CMM.Survey.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
namespace CMM.Survey.SurveyFields
{
	public class ComboBox : BaseQuestion
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass8
		{
			public string answerText;
			
			public c__DisplayClass8()
			{
				
				
			}
			
			public bool GetPostAnswerb__6(HtmlNode o)
			{
				return o.InnerText == this.answerText;
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass13
		{
			public SurveyAnswer answerValue;
			
			public c__DisplayClass13()
			{
				
				
			}
			
			public bool SetAnswerb__e(HtmlNode o)
			{
				return o.identity() == this.answerValue.AnswerHtmlId;
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private static Func<HtmlNode, bool> ZFO9vRA3Y;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private static Func<SurveyAnswer, bool> d3jsxpgt1;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private static Func<SurveyAnswer, bool> b8sVh8SUV;
		
		static ComboBox()
		{

            HtmlNode.ElementsFlags.Remove("option");
		}
		
		public ComboBox(HtmlNode node) : base (node)
		{

		}
		
		private System.Collections.Generic.List<HtmlNode> g15ynZap8()
		{
			HtmlNode htmlNode = this.LkxeOZVkQ();
			HtmlNodeCollection arg_3A_0 = htmlNode.ChildNodes;
			if (ComboBox.ZFO9vRA3Y == null)
			{
				ComboBox.ZFO9vRA3Y = new Func<HtmlNode, bool>(ComboBox.x2PI0DgiM);
			}
			return arg_3A_0.search(ComboBox.ZFO9vRA3Y);
		}
		
		private HtmlNode LkxeOZVkQ()
		{
            HtmlNode htmlNode = base.Node.ChildNodes.search("wrap").First<HtmlNode>();
            return (htmlNode == null) ? null : htmlNode.ChildNodes.first("select");
		}
		
		public override System.Collections.Generic.List<SurveyAnswer> GetAnswerOptions()
		{
			System.Collections.Generic.List<SurveyAnswer> list = new System.Collections.Generic.List<SurveyAnswer>();
			System.Collections.Generic.List<HtmlNode> list2 = this.g15ynZap8();
			foreach (HtmlNode current in list2)
			{
				list.Add(new SurveyAnswer
				{
					AnswerType = SurveyAnswerType.Option, 
					AnswerHtmlId = current.identity(), 
					AnswerText = current.InnerText
				});
			}
			return list;
		}
		
		public override System.Collections.Generic.List<SurveyAnswer> GetPostAnswer()
		{
			System.Collections.Generic.List<SurveyAnswer> list;
			NameValueCollection httpPost;
			
			list = new System.Collections.Generic.List<SurveyAnswer>();
			httpPost = base.GetHttpPost();
			if (httpPost.Count <= 0)
			{
				goto IL_134;
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
			
			System.Collections.Generic.List<HtmlNode> source = this.g15ynZap8();
            c__DisplayClass8 c__DisplayClass = new c__DisplayClass8();

			c__DisplayClass.answerText = httpPost[base.FieldName];
			if (!string.IsNullOrEmpty(c__DisplayClass.answerText))
			{
				HtmlNode node = source.Where(new Func<HtmlNode, bool>(c__DisplayClass.GetPostAnswerb__6)).First<HtmlNode>();
				list.Add(new SurveyAnswer
				{
					AnswerType = SurveyAnswerType.Option, 
					AnswerHtmlId = node.identity(), 
					AnswerText = c__DisplayClass.answerText
				});
			}
			IL_134:
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
			System.Collections.Generic.List<HtmlNode> list2 = this.g15ynZap8();
			foreach (HtmlNode current in list2)
			{
				if (current.hasAttr("selected"))
				{
					string innerText = current.InnerText;
					if (!string.IsNullOrEmpty(innerText))
					{
						list.Add(new SurveyAnswer
						{
							AnswerType = SurveyAnswerType.Option, 
							AnswerHtmlId = current.identity(), 
							AnswerText = innerText
						});
					}
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
			ComboBox.c__DisplayClass13 c__DisplayClass = new ComboBox.c__DisplayClass13();
			ComboBox.c__DisplayClass13 arg_50_0 = c__DisplayClass;
			if (ComboBox.d3jsxpgt1 == null)
			{
				ComboBox.d3jsxpgt1 = new Func<SurveyAnswer, bool>(ComboBox.FACGSbJTq);
			}
			arg_50_0.answerValue = value.Where(ComboBox.d3jsxpgt1).FirstOrDefault<SurveyAnswer>();
			if (c__DisplayClass.answerValue != null)
			{
				HtmlNode htmlNode = this.g15ynZap8().Where(new Func<HtmlNode, bool>(c__DisplayClass.SetAnswerb__e)).FirstOrDefault<HtmlNode>();
				if (htmlNode != null)
				{
					htmlNode.attr("selected", "true");
				}
			}
			if (ComboBox.b8sVh8SUV == null)
			{
				ComboBox.b8sVh8SUV = new Func<SurveyAnswer, bool>(ComboBox.RidRktHXR);
			}
			SurveyAnswer surveyAnswer = value.Where(ComboBox.b8sVh8SUV).FirstOrDefault<SurveyAnswer>();
			if (surveyAnswer != null)
			{
				base.SetCommentText(surveyAnswer.AnswerText);
			}			
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		
		private static bool x2PI0DgiM(HtmlNode node)
		{
            return node.tag() == "OPTION";
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		
		private static bool FACGSbJTq(SurveyAnswer surveyAnswer)
		{
			return surveyAnswer.AnswerType == SurveyAnswerType.Option;
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		
		private static bool RidRktHXR(SurveyAnswer surveyAnswer)
		{
			return surveyAnswer.AnswerType == SurveyAnswerType.Comment;
		}
	}
}
