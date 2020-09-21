
using HtmlAgilityPack;
using System;
using System.Runtime.CompilerServices;
namespace CMM.Survey.SurveyFields
{
	public class Thanks : BaseField, ISurveyThanks, ISurveyField
	{
		
		public Thanks(HtmlNode node) : base (node)
		{
			
		}
	}
}
