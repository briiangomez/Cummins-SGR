

using HtmlAgilityPack;
using CMM.Survey.Models;
using CMM.Survey.SurveyWriters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
namespace CMM.Survey
{
	public class SurveyFormClass
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass3
		{
			public System.Collections.Generic.Dictionary<string, ISurveyQuestion> dict;
			
			public c__DisplayClass3()
			{
				
				
			}
			
			public void RevertAnswerb__2(ISurveyQuestion q)
			{
				this.dict.Add(q.FieldName, q);
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass6
		{
			public System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<SurveyAnswer>> dict;
			
			public c__DisplayClass6()
			{
				
				
			}
			
			public void GetAnswerOptionsb__5(ISurveyQuestion q)
			{
				this.dict.Add(q.FieldName, q.GetAnswerOptions());
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClassc
		{
			public System.Collections.Generic.List<SurveyQuestion> questions = new List<SurveyQuestion>();
			
			public c__DisplayClassc()
			{
				
				
			}
			
			public void GetQuestionsb__8(ISurveyQuestion q)
			{
				this.questions.Add(q.GetQuestion());
			}
			
			public void GetQuestionsb__9(ISurveyQuestion q)
			{
				this.questions.Add(q.GetQuestion());
			}
		}
		public const string FormIdKeyName = "__formId";
		public const string PageIndexKeyName = "__pageIndex";
		public const string PageCountKeyName = "__pageCount";
		private ISurveyHeader LS1sJUxaS;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private SurveyContext ix6qDcAft;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private HtmlNode YvUUlWNth;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private SurveyFieldBox ingZuGAyl;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private static Func<ISurveyField, bool> ingvxS1bp;
		public SurveyContext Context
		{
			
			get;
			
			private set;
		}
		public HtmlNode Node
		{
			
			get;
			
			private set;
		}
		public SurveyFieldBox FieldBox
		{
			
			get;
			
			private set;
		}
		public int PageCount
		{
			
			get
			{
				return this.FieldBox.BreakCount + 1;
			}
		}
		public int PageIndex
		{
			
			get
			{
				return this.Context.PageIndex;
			}
		}
		public bool PageIndexOverflow
		{
			
			get
			{
				return this.PageIndex <= -1 || this.PageIndex >= this.PageCount;
			}
		}
		
		public SurveyFormClass(SurveyContext context)
		{	
			if (context == null)
			{
				throw new System.ArgumentNullException();
			}
			this.Context = context;
			this.Context.Handler = this;
			this.Node = HtmlNode.CreateNode((this.Context.FormHtml ?? string.Empty).Trim());
			this.FieldBox = SurveyFieldKit.ParseFieldBox(this.Node);
		}
		
		public string GetFormTitle()
		{
			string result;
			if (this.LS1sJUxaS == null)
			{
				System.Collections.Generic.IEnumerable<ISurveyField> arg_48_0 = this.FieldBox;
				if (SurveyFormClass.ingvxS1bp == null)
				{
					SurveyFormClass.ingvxS1bp = new Func<ISurveyField, bool>(SurveyFormClass.O7defpKWU);
				}
				this.LS1sJUxaS = arg_48_0.Where(SurveyFormClass.ingvxS1bp).Cast<ISurveyHeader>().FirstOrDefault<ISurveyHeader>();
				if (this.LS1sJUxaS == null)
				{
					result = null;
					return result;
				}
			}
			result = this.LS1sJUxaS.GetSurveyTitle();
			return result;
		}
		
		public void RevertAnswer(System.Collections.Generic.IEnumerable<SurveyQuestion> values)
		{
			SurveyFormClass.c__DisplayClass3 c__DisplayClass = new SurveyFormClass.c__DisplayClass3();
			c__DisplayClass.dict = new System.Collections.Generic.Dictionary<string, ISurveyQuestion>();
			this.FieldBox.LoopQuestions(new System.Action<ISurveyQuestion>(c__DisplayClass.RevertAnswerb__2), null);
			foreach (SurveyQuestion current in values)
			{
				if (c__DisplayClass.dict.ContainsKey(current.QuestionHtmlId))
				{
					c__DisplayClass.dict[current.QuestionHtmlId].SetAnswer(current.Answers);
				}
			}			
		}
		
		public System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<SurveyAnswer>> GetAnswerOptions()
		{
			SurveyFormClass.c__DisplayClass6 c__DisplayClass = new SurveyFormClass.c__DisplayClass6();
			c__DisplayClass.dict = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<SurveyAnswer>>();
			this.FieldBox.LoopQuestions(new System.Action<ISurveyQuestion>(c__DisplayClass.GetAnswerOptionsb__5), null);
			return c__DisplayClass.dict;
		}
		
		public System.Collections.Generic.List<SurveyQuestion> GetQuestions(int pageIndex = -1)
		{
			bool flag;

            c__DisplayClassc c__DisplayClassc = new SurveyFormClass.c__DisplayClassc();
			
			System.Collections.Generic.List<SurveyQuestion> questions = new System.Collections.Generic.List<SurveyQuestion>();
			flag = (pageIndex >= 0);
			
			if (!flag)
			{
				this.FieldBox.LoopQuestions(new System.Action<ISurveyQuestion>(c__DisplayClassc.GetQuestionsb__8), null);
			}
			else
			{
				this.FieldBox.LoopQuestions(new System.Action<ISurveyQuestion>(c__DisplayClassc.GetQuestionsb__9), new int?(pageIndex));
			}
			return c__DisplayClassc.questions;
		}
		
		private ISurveyWriter ingyIR1EL()
		{
			ISurveyWriter result;
			
			string a = (this.Context.FormMethod ?? string.Empty).ToLower();
			if (this.Context.Writer != null)
			{
				result = this.Context.Writer;
				return result;
			}
            if (!(a == "get"))
			{
                if (a == "post")
				{
					result = new PostWriter();
					return result;
				}
				result = new EmptyWriter();
				return result;
			}
			
			result = new GetWriter();
			return result;
		}
		
		public string Render()
		{
			System.Text.StringBuilder stringBuilder;
			System.IO.StringWriter stringWriter;
			ISurveyWriter surveyWriter;

            this.Context.Items.Add("__pageCount", this.PageCount);
            this.Context.Items.Add("__pageIndex", this.PageIndex);
            this.Context.Items.Add("__formId", this.Context.FormId);
			stringBuilder = new System.Text.StringBuilder();
			stringWriter = new System.IO.StringWriter(stringBuilder);
			surveyWriter = this.ingyIR1EL();
			
			surveyWriter.Initialize(this);
			surveyWriter.Write(stringWriter);
			stringWriter.Flush();
			stringWriter.Dispose();
			return stringBuilder.ToString();
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		
		private static bool O7defpKWU(ISurveyField surveyField)
		{
			return surveyField is ISurveyHeader;
		}
	}
}
