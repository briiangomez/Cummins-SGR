using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
namespace CMM.Survey
{
	public static class Extensions
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass1
		{
			public string v;
			
			public c__DisplayClass1()
			{
				
				
			}
			
			public void valb__0(HtmlNode n)
			{
                if (n.tag() == "OPTION")
				{
                    if (n.hasAttr("selected"))
					{
                        this.v = n.attr("value");
						if (string.IsNullOrEmpty(this.v))
						{
							this.v = n.InnerText;
						}
					}
				}
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass5
		{
			public string value;
			
			public c__DisplayClass5()
			{
				
				
			}
			
			public void valb__3(HtmlNode n)
			{
                if (n.tag() == "OPTION")
				{
                    if (n.attr("value") == this.value)
					{
                        n.attr("selected", "true");
					}
					else
					{
						if (n.InnerText == this.value)
						{
                            n.attr("selected", "true");
						}
					}
				}				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass8
		{
			public string className;
			
			public c__DisplayClass8()
			{
				
				
			}
			
			public bool searchb__7(HtmlNode n)
			{
				return n.hasClass(this.className);
			}
		}
		
		public static void remove(this HtmlNode node)
		{
			node.ParentNode.RemoveChild(node);
		}
		
		public static string attr(this HtmlNode node, string name)
		{
			return node.GetAttributeValue(name, null);
		}
		
		public static void attr(this HtmlNode node, string name, string value)
		{
			node.SetAttributeValue(name, value);
		}
		
		public static bool hasAttr(this HtmlNode node, string attrName)
		{
			return !string.IsNullOrEmpty(node.attr(attrName));
		}
		
		public static void removeAttr(this HtmlNode node, string attrName)
		{
			node.Attributes.Remove(attrName);
		}
		
		public static string tag(this HtmlNode node)
		{
			return node.Name.ToUpper();
		}
		
		public static string name(this HtmlNode node)
		{
            return node.attr("name");
		}
		
		public static string identity(this HtmlNode node)
		{
            return node.attr("id");
		}
		
		public static string val(this HtmlNode node)
		{
			string a;
			string result;
			bool flag;
			
			a = node.tag();
            if (a == "INPUT")
			{
                string a2 = node.attr("INPUT");
                if (a2 == "checkbox" || a2 == "radio")
				{
                    if (node.hasAttr("checked"))
					{
                        result = node.attr("value");
						return result;
					}
				}
                result = node.attr("value");
				return result;
			}
            flag = !(a == "TEXTAREA");
			
			if (!flag)
			{
				result = node.InnerHtml;
			}
			else
			{
                if (a == "SELECT")
				{
					Extensions.c__DisplayClass1 c__DisplayClass = new Extensions.c__DisplayClass1();
					c__DisplayClass.v = string.Empty;
					node.visit(new System.Action<HtmlNode>(c__DisplayClass.valb__0));
					result = c__DisplayClass.v;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}
		
		public static void val(this HtmlNode node, string value)
		{
			Extensions.c__DisplayClass5 c__DisplayClass = new Extensions.c__DisplayClass5();
			c__DisplayClass.value = value;
			string a = node.tag();
            if (a == "INPUT")
			{
                string a2 = node.attr("type");
                if (a2 == "checkbox" || a2 == "radio")
				{
                    if (node.attr("value") == c__DisplayClass.value)
					{
                        node.attr("checked", "true");
					}
				}
				else
				{
                    node.attr("value", c__DisplayClass.value);
				}
			}
			else
			{
                if (a == "TEXTAREA")
				{
					node.InnerHtml = c__DisplayClass.value;
				}
				else
				{
                    if (a == "SELECT")
					{
						node.visit(new System.Action<HtmlNode>(c__DisplayClass.valb__3));
					}
				}
			}			
		}
		
		public static bool hasClass(this HtmlNode node, string cssName)
		{
			bool result;
            if (node.Attributes["class"] == null || string.IsNullOrEmpty(cssName))
			{
				result = false;
			}
			else
			{
                string text = node.Attributes["class"].Value.ToLower();
                string[] source = text.Split(" ".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
				result = source.Contains(cssName.ToLower());
			}
			return result;
		}
		
        public static bool invisible(this HtmlNode node, bool defaultInvisible)
        {
            if (defaultInvisible)
            {
                if (node.Attributes["style"] == null)
                {
                    return true;
                }
                string str = node.Attributes["style"].Value.Replace(" ", string.Empty).ToLower();
                return ((!str.Contains("display:block") && !str.Contains("display:inline")) && !str.Contains("display:inline-block"));
            }
            if (node.Attributes["style"] == null)
            {
                return false;
            }
            return node.Attributes["style"].Value.Replace(" ", string.Empty).ToLower().Contains("display:none");
        }

		
		public static void visit(this HtmlNode node, System.Action<HtmlNode> action)
		{
			if (action != null)
			{
				action(node);
				if (node.ChildNodes != null)
				{
					for (int i = 0; i < node.ChildNodes.Count; i++)
					{
						node.ChildNodes[i].visit(action);
					}
				}
			}
		}
		
		public static bool visit(this HtmlNode node, Func<HtmlNode, bool> func)
		{
			bool result;
			bool flag;
			int num;
			
			if (func == null)
			{
				result = false;
				return result;
			}
			flag = func(node);
			if (flag && node.ChildNodes != null)
			{
				num = 0;
				goto IL_85;
			}
			goto IL_9B;
			
			IL_72:
			if (!flag)
			{
				goto IL_9A;
			}
			num++;
			IL_85:
			if (num < node.ChildNodes.Count)
			{
				flag = node.ChildNodes[num].visit(func);
				goto IL_72;
			}
			IL_9A:
			IL_9B:
			result = flag;
			return result;
		}
		
		public static System.Collections.Generic.List<HtmlNode> search(this HtmlNodeCollection nodes, Func<HtmlNode, bool> filter)
		{
			System.Collections.Generic.List<HtmlNode> list = new System.Collections.Generic.List<HtmlNode>();
			foreach (HtmlNode current in (System.Collections.Generic.IEnumerable<HtmlNode>)nodes)
			{
				if (filter(current))
				{
					list.Add(current);
				}
			}
			return list;
		}
		
		public static System.Collections.Generic.List<HtmlNode> search(this HtmlNodeCollection nodes, string className)
		{
			Extensions.c__DisplayClass8 c__DisplayClass = new Extensions.c__DisplayClass8();
			c__DisplayClass.className = className;
			return nodes.search(new Func<HtmlNode, bool>(c__DisplayClass.searchb__7));
		}
		
		public static HtmlNode first(this HtmlNodeCollection nodes)
		{
			HtmlNode result;
			foreach (HtmlNode current in (System.Collections.Generic.IEnumerable<HtmlNode>)nodes)
			{
				if (current.NodeType == HtmlNodeType.Element)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}
		
		public static HtmlNode first(this HtmlNodeCollection nodes, string tag)
		{
			HtmlNode result;
			foreach (HtmlNode current in (System.Collections.Generic.IEnumerable<HtmlNode>)nodes)
			{
				if (current.NodeType == HtmlNodeType.Element && current.tag() == tag.ToUpper())
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}
		
		public static HtmlNode eq(this HtmlNodeCollection nodes, string tag, int index)
		{
			int num = -1;
			HtmlNode result;
			foreach (HtmlNode current in (System.Collections.Generic.IEnumerable<HtmlNode>)nodes)
			{
				if (current.tag() == tag.ToUpper())
				{
					num++;
					if (num == index)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}
		
		public static void Render(this System.Collections.Generic.IEnumerable<ISurveyField> fields, System.IO.TextWriter writer)
		{
			if (fields != null)
			{
				foreach (ISurveyField current in fields)
				{
					current.Render(writer);
				}
			}
		}
	}
}
