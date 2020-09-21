
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace CMM.Survey.Models
{
	public class SurveyQuestion
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private System.Guid inge7CRv4;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string wr6IGLmNk;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string s0aEkT72q;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private int asjR9Bo1P;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private bool iera20Phe;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private bool ings97FBp;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private System.Collections.Generic.List<SurveyAnswer> ufbW2gPqO;
		public System.Guid QuestionId
		{
			
			get;
			
			set;
		}
		public string QuestionHtmlId
		{
			
			get;
			
			set;
		}
		public string QuestionText
		{
			
			get;
			
			set;
		}
		public int QuestionIndex
		{
			
			get;
			
			set;
		}
		public bool IsRequired
		{
			
			get;
			
			set;
		}
		public bool AllowComment
		{
			
			get;
			
			set;
		}
		public System.Collections.Generic.List<SurveyAnswer> Answers
		{
			
			get;
			
			private set;
		}
		
		public SurveyQuestion()
		{
			
			
			this.Answers = new System.Collections.Generic.List<SurveyAnswer>();
		}
	}
}
