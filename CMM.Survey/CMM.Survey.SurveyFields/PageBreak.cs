using HtmlAgilityPack;
using System;
using System.IO;
using System.Runtime.CompilerServices;
namespace CMM.Survey.SurveyFields
{
	public class PageBreak : BaseField, ISurveyBreak, ISurveyField
	{
		
		public PageBreak(HtmlNode node) : base(node)
		{

		}
		
		public override void Render(System.IO.TextWriter writer)
		{
		}
	}
}
