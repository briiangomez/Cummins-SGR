using CMM.Survey.Models;
using System;
using System.Runtime.CompilerServices;
namespace CMM.Survey.ModelsDb
{
	public class Survey_Answer
	{
		public System.Guid Id
		{
			
			get;
			
			set;
		}
		public System.Guid QuestionId
		{
			
			get;
			
			set;
		}
		public System.Guid PostDataId
		{
			
			get;
			
			set;
		}
		public string AnswerHtmlId
		{
			
			get;
			
			set;
		}
		public int? AnswerType
		{
			
			get;
			
			set;
		}
		public string AnswerText
		{
			
			get;
			
			set;
		}
		public string CommentText
		{
			
			get;
			
			set;
		}
		public SurveyAnswerType AnswerTypeEnum
		{
			
			get
			{
				int? answerType = this.AnswerType;
				SurveyAnswerType result;
				if (answerType.HasValue)
				{
					answerType = this.AnswerType;
					result = (SurveyAnswerType)answerType.Value;
				}
				else
				{
					result = SurveyAnswerType.Unset;
				}
				return result;
			}
		}
		public Survey_PostData Survey_PostData
		{
			
			get;
			
			set;
		}
		public Survey_Question Survey_Question
		{
			
			get;
			
			set;
		}
		
		public Survey_Answer()
		{
			
			
		}
	}
}
