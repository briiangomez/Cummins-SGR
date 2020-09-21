
using CMM.Survey.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace CMM.Survey.ModelsDb
{
	public class Survey_PostData
	{
		public System.Guid Id
		{
			
			get;
			
			set;
		}
		public System.Guid FormId
		{
			
			get;
			
			set;
		}
		public string ContactId
		{
			
			get;
			
			set;
		}
		public System.DateTime PostDate
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
		public int? SourceType
		{
			
			get;
			
			set;
		}
		public Survey_Form Survey_Form
		{
			
			get;
			
			set;
		}
		public System.Collections.Generic.List<Survey_Answer> Survey_Answer
		{
			
			get;
			
			set;
		}
		public SurveyPostSource SourceTypeEnum
		{
			
			get
			{
				int? sourceType = this.SourceType;
				SurveyPostSource arg_2F_0;
				if (!sourceType.HasValue)
				{
					arg_2F_0 = SurveyPostSource.Form;
				}
				else
				{
					sourceType = this.SourceType;
					arg_2F_0 = (SurveyPostSource)sourceType.Value;
				}
				return arg_2F_0;
			}
		}
		
		public Survey_PostData()
		{
			
			
		}
	}
}
