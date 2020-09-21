using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CMM.Survey.SurveyFields;
namespace CMM.Survey
{
	public class SurveyFieldKit
	{
		private static System.Collections.Generic.Dictionary<string, System.Type> ingyIR1EL;
		
		static SurveyFieldKit()
		{
			SurveyFieldKit.ingyIR1EL = new System.Collections.Generic.Dictionary<string, System.Type>();
			
            ingyIR1EL.Add("checkList", typeof(ChcekList));
            ingyIR1EL.Add("comboBox", typeof(ComboBox));
            ingyIR1EL.Add("copyright", typeof(Copyright));
            ingyIR1EL.Add("formInfo", typeof(FormInfo));
            ingyIR1EL.Add("pageBreak", typeof(PageBreak));
            ingyIR1EL.Add("greeting", typeof(Greeting));
            ingyIR1EL.Add("radioList", typeof(RadioList));
            ingyIR1EL.Add("rateItems", typeof(RateItems));
            ingyIR1EL.Add("textArea", typeof(TextArea));
            ingyIR1EL.Add("textBox", typeof(TextBox));
            ingyIR1EL.Add("textLabel", typeof(TextLabel));
            ingyIR1EL.Add("thanks", typeof(Thanks));
            ingyIR1EL.Add("*", typeof(Undefined));

		}
		
		public void Register(string typeKey, System.Type type)
		{   
            if (!type.GetInterfaces().Contains<Type>(typeof(ISurveyField)))
            {
                throw new ArgumentException(string.Format("Type:{0} you register is not a {1} type.", type.FullName, typeof(ISurveyField).Name));
            }
            ingyIR1EL[typeKey] = type;

		}
		
		public static bool UnRegister(string typeKey)
		{
			return SurveyFieldKit.ingyIR1EL.Remove(typeKey);
		}
		
		public static void UnRegisterAll()
		{
			SurveyFieldKit.ingyIR1EL.Clear();
		}
		
		public static SurveyFieldBox ParseFieldBox(HtmlNode node)
		{
			SurveyFieldBox surveyFieldBox;
			HtmlNodeCollection childNodes;
			int num;
			
			surveyFieldBox = new SurveyFieldBox();
			if (node != null && node.ParentNode != null)
			{
				childNodes = node.ParentNode.ChildNodes;
				num = 0;
				goto IL_98;
			}
			return surveyFieldBox;
			
			IL_7B:
			ISurveyField surveyField;
			if (surveyField != null)
			{
				surveyFieldBox.Add(surveyField);
			}
			IL_93:
			num++;
			IL_98:
			if (num >= childNodes.Count)
			{
				return surveyFieldBox;
			}
			HtmlNode htmlNode = childNodes[num];
			if (htmlNode.NodeType == HtmlNodeType.Element)
			{
				surveyField = SurveyFieldKit.ParseField(htmlNode);
				goto IL_7B;
			}
			goto IL_93;
		}
		
		public static ISurveyField ParseField(HtmlNode node)
		{
            string text = node.attr("typeKey");
			if (string.IsNullOrEmpty(text))
			{
				text = "*";
			}
			if (!SurveyFieldKit.ingyIR1EL.ContainsKey(text))
			{
                throw new System.Exception(string.Format("Dictionary can not find the typeKey:{0}.", text));
			}
			return (ISurveyField)System.Activator.CreateInstance(SurveyFieldKit.ingyIR1EL[text], new object[]
			{
				node
			});
		}
		
		public SurveyFieldKit()
		{
			
			
		}
	}
}
