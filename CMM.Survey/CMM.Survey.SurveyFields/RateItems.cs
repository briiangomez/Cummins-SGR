

using HtmlAgilityPack;
using CMM.Survey.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
namespace CMM.Survey.SurveyFields
{
	public class RateItems : BaseQuestion
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass1
		{
			public HtmlNode tr;
			
			public c__DisplayClass1()
			{
				
				
			}
			
			public bool GetFirstTrb__0(HtmlNode n)
			{
				bool result;
                if (n.tag() == "TR")
				{
					this.tr = n;
					result = false;
				}
				else
				{
					result = true;
				}
				return result;
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClassd
		{
			public HtmlNode radio;
			
			public c__DisplayClassd()
			{
				
				
			}
			
			public bool SetAnswerb__a(SurveyAnswer o)
			{
				return o.AnswerType == SurveyAnswerType.Option && o.AnswerHtmlId == this.radio.identity();
			}
		}
		public const string ZeroWidthSpace = "&#8203;";
		public const string Separacotr = "&#8203;:&#8203;";
		[System.Runtime.CompilerServices.CompilerGenerated]
		private static Func<SurveyAnswer, bool> wex7jJGBa;
		
		public RateItems(HtmlNode node) : base (node)
		{

		}
		
		private HtmlNode wRsyirdC7(int num, int num2)
		{
			num++;
			num2++;
			HtmlNode htmlNode = this.gOMA2Do5u();
            return htmlNode.ParentNode.ChildNodes.eq("TR", num).ChildNodes.eq("TD", num2).ChildNodes.first();
		}
		
		private System.Collections.Generic.List<HtmlNode> ingeV6FrT()
		{
			HtmlNode htmlNode;
			System.Collections.Generic.List<HtmlNode> list;
			int num;
			
			htmlNode = this.gOMA2Do5u();
			list = new System.Collections.Generic.List<HtmlNode>();
			num = 0;
			goto IL_6B;
			
			IL_64:
			IL_66:
			num++;
			IL_6B:
			if (num >= htmlNode.ChildNodes.Count)
			{
				list.RemoveAt(0);
				return list;
			}
			if (htmlNode.ChildNodes[num].NodeType == HtmlNodeType.Element)
			{
				list.Add(htmlNode.ChildNodes[num]);
				goto IL_64;
			}
			goto IL_66;
		}
		
		private System.Collections.Generic.List<HtmlNode> ingI7Sana()
		{
			HtmlNode htmlNode = this.gOMA2Do5u();
			System.Collections.Generic.List<HtmlNode> list = new System.Collections.Generic.List<HtmlNode>();
			while (htmlNode.NextSibling != null)
			{
				if (htmlNode.NextSibling.NodeType == HtmlNodeType.Element)
				{
                    list.Add(htmlNode.NextSibling.ChildNodes.first("TD"));
				}
				htmlNode = htmlNode.NextSibling;
			}
			return list;
		}
		
		private HtmlNode gOMA2Do5u()
		{
			RateItems.c__DisplayClass1 c__DisplayClass = new RateItems.c__DisplayClass1();
			c__DisplayClass.tr = null;
			base.Node.visit(new Func<HtmlNode, bool>(c__DisplayClass.GetFirstTrb__0));
			return c__DisplayClass.tr;
		}
		
		public static string RemoveSplit(string answerText)
		{
			string result;
			if (string.IsNullOrEmpty(answerText))
			{
				result = answerText;
			}
			else
			{
                result = answerText.Replace("&#8203;", string.Empty);
			}
			return result;
		}
		
		public static string SplitValue(string answerText)
		{
			string result;
			if (string.IsNullOrEmpty(answerText))
			{
				result = answerText;
			}
			else
			{
                if (answerText.IndexOf("&#8203;:&#8203;") > -1)
				{
					result = answerText.Split(new string[]
					{
						"&#8203;:&#8203;"
					}, System.StringSplitOptions.RemoveEmptyEntries)[1];
				}
				else
				{
					result = answerText;
				}
			}
			return result;
		}
		
		public static string CombineValue(string attr, string rate)
		{
            return attr + "&#8203;:&#8203;" + rate;
		}
		
		public override System.Collections.Generic.List<SurveyAnswer> GetAnswerOptions()
		{
			System.Collections.Generic.List<SurveyAnswer> list;
			System.Collections.Generic.List<HtmlNode> list2;
			System.Collections.Generic.List<HtmlNode> list3;
			int num;
			
			list = new System.Collections.Generic.List<SurveyAnswer>();
			list2 = this.ingeV6FrT();
			list3 = this.ingI7Sana();
			num = 0;
			goto IL_BD;
			
			IL_9D:
			int num2;
			num2++;
			IL_A5:
			if (num2 < list2.Count)
			{
				HtmlNode node = this.wRsyirdC7(num, num2);
				string answerText = RateItems.CombineValue(list3[num].InnerText, list2[num2].InnerText);
				list.Add(new SurveyAnswer
				{
					AnswerType = SurveyAnswerType.Option, 
					AnswerHtmlId = node.identity(), 
					AnswerText = answerText
				});
				goto IL_9D;
			}
			num++;
			IL_BD:
			if (num >= list3.Count)
			{
				return list;
			}
			num2 = 0;
			goto IL_A5;
		}
		
		public override System.Collections.Generic.List<SurveyAnswer> GetPostAnswer()
		{
            List<SurveyAnswer> list = new List<SurveyAnswer>();
            List<HtmlNode> list2 = this.ingeV6FrT();
            List<HtmlNode> list3 = this.ingI7Sana();
            NameValueCollection httpPost = base.GetHttpPost();
            if (httpPost.Count > 0)
            {
                string str = httpPost[base.CommentId];
                if (!string.IsNullOrEmpty(str))
                {
                    SurveyAnswer item = new SurveyAnswer {
                        AnswerType = SurveyAnswerType.Comment,
                        AnswerHtmlId = base.CommentId,
                        AnswerText = str
                    };
                    list.Add(item);
                    httpPost.Remove(base.CommentId);
                }
                for (int i = 0; i < list3.Count; i++)
                {
                    for (int j = 0; j < list2.Count; j++)
                    {
                        HtmlNode node = this.wRsyirdC7(i, j);
                        string str2 = node.name();
                        string str3 = node.val();

                        //if (httpPost.AllKeys.Contains<string>(str2) && (str3 == httpPost[str2]))
                        //{
                        //    httpPost.Remove(str2);
                        //    SurveyAnswer answer2 = new SurveyAnswer {
                        //        AnswerType = SurveyAnswerType.Option,
                        //        AnswerHtmlId = node.identity(),
                        //        AnswerText = CombineValue(list3[i].InnerText, list2[j].InnerText)
                        //    };
                        //    list.Add(answer2);
                        //}
                        
                        //19 12 modificado para permitir múltiples valores en los checks
                        //if (httpPost.AllKeys.Contains<string>(str2) && (str3 == httpPost[str2]))
                        if (httpPost.AllKeys.Contains<string>(str2) && (httpPost[str2].Contains(str3)))
                        {
                            if (str3 == httpPost[str2])
                                httpPost.Remove(str2);

                            SurveyAnswer answer2 = new SurveyAnswer
                            {
                                AnswerType = SurveyAnswerType.Option,
                                AnswerHtmlId = node.identity(),
                                AnswerText = CombineValue(list3[i].InnerText, list2[j].InnerText)
                            };
                            list.Add(answer2);
                        }
                    }
                }
            }
            if (list.Count == 0)
            {
                SurveyAnswer answer3 = new SurveyAnswer {
                    AnswerType = SurveyAnswerType.Empty
                };
                list.Add(answer3);
            }
            return list;
        

            //System.Collections.Generic.List<SurveyAnswer> list;
            //System.Collections.Generic.List<HtmlNode> list2;
            //System.Collections.Generic.List<HtmlNode> list3;
            //NameValueCollection httpPost;
            //int num;
			
            //list = new System.Collections.Generic.List<SurveyAnswer>();
            //list2 = this.ingeV6FrT();
            //list3 = this.ingI7Sana();
            //httpPost = base.GetHttpPost();
            //if (httpPost.Count > 0)
            //{
            //    string text = httpPost[base.CommentId];
            //    if (!string.IsNullOrEmpty(text))
            //    {
            //        list.Add(new SurveyAnswer
            //        {
            //            AnswerType = SurveyAnswerType.Comment, 
            //            AnswerHtmlId = base.CommentId, 
            //            AnswerText = text
            //        });
            //        httpPost.Remove(base.CommentId);
            //    }
            //    num = 0;
            //    goto IL_189;
            //}
            			
			
            //IL_DB:
            //string text2;
            //string a;
            //int num2;
            //if (httpPost.AllKeys.Contains(text2) && a == httpPost[text2])
            //{
            //    httpPost.Remove(text2);
            //    HtmlNode node;
            //    list.Add(new SurveyAnswer
            //    {
            //        AnswerType = SurveyAnswerType.Option, 
            //        AnswerHtmlId = node.identity(), 
            //        AnswerText = RateItems.CombineValue(list3[num].InnerText, list2[num2].InnerText)
            //    });
            //}
            //num2++;
            //IL_16F:
            //if (num2 < list2.Count)
            //{
            //    HtmlNode node = this.wRsyirdC7(num, num2);
            //    text2 = node.name();
            //    a = node.val();
            //    goto IL_DB;
            //}
            //num++;
            //IL_189:
            //if (num >= list3.Count)
            //{
            //    IL_19D:
            //    if (list.Count == 0)
            //    {
            //        list.Add(new SurveyAnswer
            //        {
            //            AnswerType = SurveyAnswerType.Empty
            //        });
            //    }
            //    return list;
            //}
            //num2 = 0;
            //goto IL_16F;
		}
		
		public override System.Collections.Generic.List<SurveyAnswer> GetAnswer()
		{
			System.Collections.Generic.List<SurveyAnswer> list;
			System.Collections.Generic.List<HtmlNode> list2;
			System.Collections.Generic.List<HtmlNode> list3;
			int num;
			
			list = new System.Collections.Generic.List<SurveyAnswer>();
			list2 = this.ingI7Sana();
			list3 = this.ingeV6FrT();
			num = 0;
			goto IL_DC;
			
			IL_D7:
			num++;
			IL_DC:
			if (num >= list2.Count)
			{
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
			for (int i = 0; i < list3.Count; i++)
			{
				HtmlNode node = this.wRsyirdC7(num, i);
                if (node.hasAttr("checked"))
				{
					list.Add(new SurveyAnswer
					{
						AnswerType = SurveyAnswerType.Option, 
						AnswerHtmlId = node.identity(), 
						AnswerText = RateItems.CombineValue(list2[num].InnerText, list3[i].InnerText)
					});
					break;
				}
			}
			goto IL_D7;
		}
		
		public override void SetAnswer(System.Collections.Generic.List<SurveyAnswer> value)
		{
			System.Collections.Generic.List<HtmlNode> list = this.ingI7Sana();
			System.Collections.Generic.List<HtmlNode> list2 = this.ingeV6FrT();
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list2.Count; j++)
				{
					RateItems.c__DisplayClassd c__DisplayClassd = new RateItems.c__DisplayClassd();
					c__DisplayClassd.radio = this.wRsyirdC7(i, j);
					SurveyAnswer surveyAnswer = value.Where(new Func<SurveyAnswer, bool>(c__DisplayClassd.SetAnswerb__a)).FirstOrDefault<SurveyAnswer>();
					if (surveyAnswer != null)
					{
                        c__DisplayClassd.radio.attr("checked", "true");
						break;
					}
				}
			}
			if (RateItems.wex7jJGBa == null)
			{
				RateItems.wex7jJGBa = new Func<SurveyAnswer, bool>(RateItems.ckWRx5Jmg);
			}
			SurveyAnswer surveyAnswer2 = value.Where(RateItems.wex7jJGBa).FirstOrDefault<SurveyAnswer>();
			if (surveyAnswer2 != null)
			{
				base.SetCommentText(surveyAnswer2.AnswerText);
			}			
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		
		private static bool ckWRx5Jmg(SurveyAnswer surveyAnswer)
		{
			return surveyAnswer.AnswerType == SurveyAnswerType.Comment;
		}
	}
}
