

using HtmlAgilityPack;
using System;
using System.Runtime.CompilerServices;
namespace CMM.Survey.SurveyFields
{
	public class Greeting : BaseField, ISurveyGreeting, ISurveyField
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private bool inge7CRv4;
		public bool ShowGreeting
		{
			
			get;
			
			private set;
		}
		
		public Greeting(HtmlNode node) : base (node)
		{
            this.ShowGreeting = (base.Node.attr("showgreeting") == "true");
		}
	}
}
