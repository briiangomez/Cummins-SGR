

using CMM.Survey.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Caching;
namespace CMM.Survey
{
	public class RuntimeKit
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass2
		{
			public System.Collections.Generic.List<SurveyQuestion> questions;
			
			public c__DisplayClass2()
			{
				
				
			}
			
			public void ProcessDatab__0(ISurveyQuestion q)
			{
				SurveyQuestion question = q.GetQuestion();
				question.Answers.AddRange(q.GetPostAnswer());
				this.questions.Add(question);
			}
		}
		public const string SourceTypeQueryName = "s";
		private static string ingyIR1EL;
		public static bool IsPortal
		{
			
			get
			{
				return true;
			}
		}
		
		public static System.Collections.Generic.List<SurveyQuestion> ProcessData(SurveyFormClass survey, int pageIndex)
		{
			RuntimeKit.c__DisplayClass2 c__DisplayClass = new RuntimeKit.c__DisplayClass2();
			c__DisplayClass.questions = new System.Collections.Generic.List<SurveyQuestion>();
			if (survey.PageCount > 0)
			{
				survey.FieldBox.LoopQuestions(new System.Action<ISurveyQuestion>(c__DisplayClass.ProcessDatab__0), new int?(pageIndex));
			}
			return c__DisplayClass.questions;
		}
		
		public static SurveyPost MergeData(System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<SurveyQuestion>> datas, System.Guid dataId)
		{
			SurveyPost surveyPost = new SurveyPost();
			surveyPost.DataId = dataId;
			foreach (System.Collections.Generic.KeyValuePair<int, System.Collections.Generic.List<SurveyQuestion>> current in datas)
			{
				surveyPost.FieldsData.AddRange(current.Value);
			}
			return surveyPost;
		}
		
        public static string AbsoluteAction(string relativeAction)
        {
            Uri url = HttpContext.Current.Request.Url;
            return string.Format("{0}://{1}{2}", url.Scheme, url.Authority, relativeAction);
        }
		
		public static void ClearClientCache(HttpResponseBase response)
        {
            response.Expires = -1;
            response.Cache.SetNoServerCaching();
            response.Cache.SetAllowResponseInBrowserHistory(false);
            response.AddHeader("pragma", "no-cache");
            response.CacheControl = "no-cache";
            response.Cache.SetNoStore();
        }
				

		
        public static void GenerateRuntimeCss()
        {
            if (!IsPortal)
            {
                DateTime now = DateTime.Now;
                Cache cache = HttpContext.Current.Cache;
                DateTime? nullable = (DateTime?)cache.Get(ingyIR1EL);
                if (!nullable.HasValue)
                {
                    nullable = new DateTime?(now);
                    cache.Insert(ingyIR1EL, nullable);
                }
                if (nullable.Value.AddHours(12.0) >= now)
                {
                    cache.Insert(ingyIR1EL, now);
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Scripts\survey\builder\field");
                    string str2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Scripts\survey\runtime\field.css");
                    string str3 = "/********** runtime style start (don't change this separator) **********/";
                    string str4 = "/********** runtime style end (don't change this separator) **********/";
                    StringBuilder builder = new StringBuilder();
                    builder.Append(Environment.NewLine).Append(Environment.NewLine);
                    builder.Append("/* This file is generated automatic by program */");
                    builder.Append(Environment.NewLine).Append(Environment.NewLine).Append(Environment.NewLine);
                    string[] files = Directory.GetFiles(path, "*.css");
                    foreach (string str5 in files)
                    {
                        string str6 = File.ReadAllText(str5);
                        int startIndex = str6.IndexOf(str3) + str3.Length;
                        int index = str6.IndexOf(str4);
                        if ((startIndex > 0) && (index > 0))
                        {
                            string str7 = str6.Substring(startIndex, index - startIndex);
                            builder.Append(string.Format("/********** {0} **********/", Path.GetFileName(str5)));
                            builder.Append(Environment.NewLine);
                            builder.Append(str7.Trim());
                            builder.Append(Environment.NewLine);
                            builder.Append(Environment.NewLine);
                        }
                    }
                    if (File.Exists(str2))
                    {
                        File.Delete(str2);
                    }
                    File.WriteAllText(str2, builder.ToString());
                }
            }
        }
		
		public RuntimeKit()
		{
			
			
		}
		
		static RuntimeKit()
		{
			// Note: this type is marked as 'beforefieldinit'.
			
			RuntimeKit.ingyIR1EL = System.Guid.NewGuid().ToString();
		}
	}
}
