
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace CMM.Survey.Models
{
	public class SurveyPost
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private System.Guid Zejy4rWRw;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string inge7CRv4;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string wr6IGLmNk;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string s0aEkT72q;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string asjR9Bo1P;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string iera20Phe;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private SurveyPostSource ings97FBp;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private System.Collections.Generic.List<SurveyQuestion> ufbW2gPqO;
		public System.Guid DataId
		{
			
			get;
			
			set;
		}
		public string ContactId
		{
			
			get;
			
			set;
		}
		public string ClientIP
		{
			
			get;
			
			set;
		}
		public string ClientBrowser
		{
			
			get;
			
			set;
		}
		public string ClientPlatform
		{
			
			get;
			
			set;
		}
		public string ValidToken
		{
			
			get;
			
			set;
		}
		public SurveyPostSource SourceType
		{
			
			get;
			
			set;
		}
		public System.Collections.Generic.List<SurveyQuestion> FieldsData
		{
			
			get;
			
			set;
		}
		
		public SurveyPost()
		{
			
			
			this.FieldsData = new System.Collections.Generic.List<SurveyQuestion>();
		}
	}
}
