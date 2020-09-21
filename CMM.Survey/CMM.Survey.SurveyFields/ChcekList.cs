

using HtmlAgilityPack;
using CMM.Survey.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
namespace CMM.Survey.SurveyFields
{
    public class ChcekList : BaseQuestion
    {
            private sealed class c__DisplayClass11
            {
                // Fields
                public List<HtmlNode> checks;
                public List<SurveyAnswer> val;

                // Methods
                public c__DisplayClass11()
                {

                }
                
                public void GetPostAnswerb__e(string answerText)
                {
                    nDfL6DAVpQRFDII5Hu hu = new nDfL6DAVpQRFDII5Hu {
                        rrfeN1x5h = this,
                        BSeIAp6DG = answerText
                    };
                    HtmlNode node = this.checks.Where<HtmlNode>(new Func<HtmlNode, bool>(hu.YlFylqmeg)).First<HtmlNode>();
                    SurveyAnswer item = new SurveyAnswer {
                        AnswerType = SurveyAnswerType.Option,
                        AnswerHtmlId = node.identity(),
                        AnswerText = hu.BSeIAp6DG
                    };
                    this.val.Add(item);
                }


                // Nested Types
                private sealed class nDfL6DAVpQRFDII5Hu
                {
                    // Fields
                    public string BSeIAp6DG;
                    public ChcekList.c__DisplayClass11 rrfeN1x5h;

                    // Methods
                    [MethodImpl(MethodImplOptions.NoInlining)]
                    public nDfL6DAVpQRFDII5Hu()
                    {

                    }

                    [MethodImpl(MethodImplOptions.NoInlining)]
                    public bool YlFylqmeg(HtmlNode node1)
                    {
                        return (node1.attr("value") == this.BSeIAp6DG);
                    }
                }
            }

            // Fields
            [CompilerGenerated]
            private static Func<SurveyAnswer, bool> ingI7Sana;

            // Methods
            [MethodImpl(MethodImplOptions.NoInlining)]
            public ChcekList(HtmlNode node) : base(node)
            {
                
            }

            [MethodImpl(MethodImplOptions.NoInlining), CompilerGenerated]
            private static bool ingeV6FrT(SurveyAnswer answer1)
            {
                return (answer1.AnswerType == SurveyAnswerType.Comment);
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            public override List<SurveyAnswer> GetAnswer()
            {
                List<SurveyAnswer> list = new List<SurveyAnswer>();
                List<HtmlNode> list2 = this.wRsyirdC7();
                foreach (HtmlNode node in list2)
                {
                    if (node.hasAttr("checked"))
                    {
                        SurveyAnswer item = new SurveyAnswer {
                            AnswerType = SurveyAnswerType.Option,
                            AnswerHtmlId = node.identity(),
                            AnswerText = node.val()
                        };
                        list.Add(item);
                    }
                }
                string commentText = base.GetCommentText();
                if (!string.IsNullOrEmpty(commentText))
                {
                    SurveyAnswer answer2 = new SurveyAnswer {
                        AnswerType = SurveyAnswerType.Comment,
                        AnswerHtmlId = base.CommentId,
                        AnswerText = commentText
                    };
                    list.Add(answer2);
                }
                if (list.Count == 0)
                {
                    SurveyAnswer answer3 = new SurveyAnswer {
                        AnswerType = SurveyAnswerType.Empty
                    };
                    list.Add(answer3);
                }
                return list;
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            public override List<SurveyAnswer> GetAnswerOptions()
            {
                List<SurveyAnswer> list = new List<SurveyAnswer>();
                List<HtmlNode> list2 = this.wRsyirdC7();
                foreach (HtmlNode node in list2)
                {
                    SurveyAnswer item = new SurveyAnswer {
                        AnswerType = SurveyAnswerType.Option,
                        AnswerHtmlId = node.identity(),
                        AnswerText = node.val()
                    };
                    list.Add(item);
                }
                return list;
            }

        //[MethodImpl(MethodImplOptions.NoInlining)]
        //public override List<SurveyAnswer> GetPostAnswer()
        //{
        //    // This item is obfuscated and can not be translated.
        //    string text1;
        //    NameValueCollection httpPost;
        //    string str;
        //    Action<string> action;

        //    action = null;
        //    List<SurveyAnswer> val = new List<SurveyAnswer>();
        //    List<HtmlNode> checks = this.wRsyirdC7();
        //    httpPost = base.GetHttpPost();
        //    if (httpPost.Count <= 0)
        //    {
        //        goto Label_0114;
        //    }
        //    str = httpPost[base.CommentId];
        //    if (string.IsNullOrEmpty(str))
        //    {
        //        goto Label_00B8;
        //    }

        //    SurveyAnswer answer = new SurveyAnswer {
        //        AnswerType = SurveyAnswerType.Comment,
        //        AnswerHtmlId = base.CommentId,
        //        AnswerText = str
        //    };
        //    val.Add(answer);
        //    httpPost.Remove(base.CommentId);
        //Label_00B8:
        //    text1 = httpPost[base.FieldName];
        //    if (text1 != null)
        //    {
        //        //goto Label_00D0;
        //    }
        //    if (action == null)
        //    {
        //        c__DisplayClass11 c_DisplayClass = new c__DisplayClass11();

        //        action = delegate(string answerText) {
        //            c_DisplayClass.GetPostAnswerb__e(answerText);
        //        };

        //        //action = delegate (string answerText) {                        
        //        //    c__DisplayClass11.nDfL6DAVpQRFDII5Hu hu = new c__DisplayClass11.nDfL6DAVpQRFDII5Hu {
        //        //        rrfeN1x5h = this,
        //        //        BSeIAp6DG = answerText
        //        //    };
        //        //    HtmlNode node = checks.Where<HtmlNode>(new Func<HtmlNode, bool>(hu.YlFylqmeg)).First<HtmlNode>();
        //        //    SurveyAnswer item = new SurveyAnswer {
        //        //        AnswerType = SurveyAnswerType.Option,
        //        //        AnswerHtmlId = node.identity(),
        //        //        AnswerText = hu.BSeIAp6DG
        //        //    };
        //        //    val.Add(item);
        //        //};
        //    }
        //    string.Empty.Split(new string[] { BaseQuestion.MultiValueSeparator }, StringSplitOptions.RemoveEmptyEntries).ToList<string>().ForEach(action);
        //Label_0114:
        //    if (val.Count == 0)
        //    {
        //        SurveyAnswer answer2 = new SurveyAnswer {
        //            AnswerType = SurveyAnswerType.Empty
        //        };
        //        val.Add(answer2);
        //    }
        //    return val;
        //}

        private List<HtmlNode> GetNodes()
        {
            var list2 = Node.ChildNodes.search("group").First().ChildNodes.search("item");
            return list2.Select(node2 => node2.ChildNodes.first("input")).ToList();
        }

        public override List<SurveyAnswer> GetPostAnswer()
        {
            /* 12/10/2013 17:09:01 */
            /* 12/10/2013 20:53:34 */
            var values = new List<SurveyAnswer>();
            var checks = GetNodes();
            var httpPost = base.GetHttpPost();

            if (httpPost.Count <= 0)
            {
                values.Add(new SurveyAnswer { AnswerType = SurveyAnswerType.Empty });
                return values;
            }
            //Se evalua se el CheckList tiene Observaciones
            if (!String.IsNullOrEmpty(httpPost[CommentId]))
            {
                values.Add(new SurveyAnswer
                {
                    AnswerType = SurveyAnswerType.Comment,
                    AnswerHtmlId = base.CommentId,
                    AnswerText = httpPost[CommentId]
                });
                httpPost.Remove(base.CommentId);
            }
            // Se evaluan Items seleccionados por el usuario.
            //* 14/10/2013 16:33:12 */
            if (httpPost[FieldName] != null)
            {
                var nodes = httpPost[FieldName].Split(new[] { MultiValueSeparator }, StringSplitOptions.RemoveEmptyEntries).ToList();
                values.AddRange(from item in nodes
                                let node = checks.First(o => o.attr("value") == item)
                                select new SurveyAnswer
                                {
                                    AnswerType = SurveyAnswerType.Option,
                                    AnswerHtmlId = node.identity(),
                                    AnswerText = item
                                });
            }
            return values;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
            public override void SetAnswer(List<SurveyAnswer> value)
            {
                List<HtmlNode> list = this.wRsyirdC7();
                using (List<HtmlNode>.Enumerator enumerator = list.GetEnumerator())
                {
                    Func<SurveyAnswer, bool> predicate = null;
                    HtmlNode input;
                    while (enumerator.MoveNext())
                    {
                        input = enumerator.Current;
                        if (predicate == null)
                        {
                            predicate = o => (o.AnswerType == SurveyAnswerType.Option) && (o.AnswerHtmlId == input.identity());
                        }
                        if (value.Where<SurveyAnswer>(predicate).FirstOrDefault<SurveyAnswer>() != null)
                        {
                            input.attr("checked", "true");
                        }
                    }
                }
                if (ingI7Sana == null)
                {
                    ingI7Sana = new Func<SurveyAnswer, bool>(ChcekList.ingeV6FrT);
                }
                SurveyAnswer answer2 = value.Where<SurveyAnswer>(ingI7Sana).FirstOrDefault<SurveyAnswer>();
                if (answer2 != null)
                {
                    base.SetCommentText(answer2.AnswerText);
                }
                
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            private List<HtmlNode> wRsyirdC7()
            {
                List<HtmlNode> list = new List<HtmlNode>();
                List<HtmlNode> list2 = base.Node.ChildNodes.search("group").First<HtmlNode>().ChildNodes.search("item");
                foreach (HtmlNode node2 in list2)
                {
                    list.Add(node2.ChildNodes.first("input"));
                }
                return list;
            }
        }
}
