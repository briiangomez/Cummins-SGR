using CMM.Survey.Models;
using System;
using System.Collections.Generic;
namespace CMM.Survey
{
	public interface ISurveyQuestion : ISurveyField
	{
		string FieldName
		{
			get;
		}
		SurveyQuestion GetQuestion();
		void SetAnswer(System.Collections.Generic.List<SurveyAnswer> value);
		System.Collections.Generic.List<SurveyAnswer> GetAnswer();
		System.Collections.Generic.List<SurveyAnswer> GetPostAnswer();
		System.Collections.Generic.List<SurveyAnswer> GetAnswerOptions();
	}
}
