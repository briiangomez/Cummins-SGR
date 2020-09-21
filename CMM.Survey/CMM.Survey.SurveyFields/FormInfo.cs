

using HtmlAgilityPack;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
namespace CMM.Survey.SurveyFields
{
	public class FormInfo : BaseField, ISurveyHeader, ISurveyField
	{
		
		public FormInfo(HtmlNode node) : base (node)
		{

		}
		
		public string GetSurveyTitle()
		{
			HtmlNode htmlNode = base.Node.ChildNodes.search("formtitle").First<HtmlNode>();
			return htmlNode.InnerText;
		}
	}
}
