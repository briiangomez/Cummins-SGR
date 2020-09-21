

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
namespace CMM.Survey.SurveyFields
{
	public abstract class BaseField : ISurveyField
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass3
		{
			public System.Collections.Generic.List<HtmlNode> nodesUseless;
			public BaseField __this;
			
			public c__DisplayClass3()
			{
				
				
			}

			public void CleanUpb__0(HtmlNode node)
			{
				if (node.NodeType == HtmlNodeType.Element)
				{
                    if (((node.tag() == "DIV") && node.hasClass("comment")) && node.invisible(true))
                    {
                        nodesUseless.Add(node);
                    }
                    if (((node.tag() == "LABEL") && node.hasClass("fieldindex")) && node.invisible(false))
                    {
                        nodesUseless.Add(node);
                    }
                    if (((node.tag() == "LABEL") && node.hasClass("require")) && node.invisible(true))
                    {
                        nodesUseless.Add(node);
                    }
                    if (((node.tag() == "DIV") && node.hasClass("fieldtop")) && node.invisible(false))
                    {
                        nodesUseless.Add(node);
                    }
                    if ((node.tag() == "LABEL") && node.hasClass("guideline"))
					{
						if (string.IsNullOrEmpty((node.InnerHtml ?? string.Empty).Trim()))
						{
							this.nodesUseless.Add(node);
						}
					}
                    node.removeAttr("uuid");
                    node.removeAttr("typeKey");
                    node.removeAttr("f_" + this.__this.gOMA2Do5u);
                    node.removeAttr("empty");
                    node.removeAttr("indexKey");
                    node.removeAttr("itemCount");
                    node.removeAttr("groupCount");
                    node.removeAttr("identityCount");
                    node.removeAttr("rowspan");
                    node.removeAttr("colspan");
                    node.removeAttr("sendemail");
                    node.removeAttr("numbering");
                    node.removeAttr("separator");
				}
			}			
		}
		private string gOMA2Do5u;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private HtmlNode ckWRx5Jmg;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string wex7jJGBa;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private static System.Action<HtmlNode> RcKsnDfL6;
		public HtmlNode Node
		{
			
			get;
			
			private set;
		}
		public string TypeKey
		{
			
			get;
			
			private set;
		}
		
		public BaseField(HtmlNode node)
		{	
			if (node == null)
			{
                throw new System.ArgumentNullException("node can not be null!");
			}
			this.Node = node;
            this.gOMA2Do5u = node.attr("uuid");
            this.TypeKey = node.attr("typeKey");
		}
		
		public virtual void CleanUp()
		{
			BaseField.c__DisplayClass3 c__DisplayClass = new BaseField.c__DisplayClass3();
			c__DisplayClass.__this = this;
			c__DisplayClass.nodesUseless = new System.Collections.Generic.List<HtmlNode>();
			this.Node.visit(new System.Action<HtmlNode>(c__DisplayClass.CleanUpb__0));
			System.Collections.Generic.List<HtmlNode> arg_63_0 = c__DisplayClass.nodesUseless;
			if (BaseField.RcKsnDfL6 == null)
			{
				BaseField.RcKsnDfL6 = new System.Action<HtmlNode>(BaseField.wRsyirdC7);
			}
			arg_63_0.ForEach(BaseField.RcKsnDfL6);
		}
		
		public virtual void Render(System.IO.TextWriter writer)
		{
			this.CleanUp();
			this.Node.WriteTo(writer);
			writer.WriteLine();
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		
		private static void wRsyirdC7(HtmlNode node)
		{
			node.remove();
		}
	}
}
