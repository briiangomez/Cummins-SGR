namespace CMM.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    public static class SelectListItemTreeExtension
    {
        public static string Indentation = "&nbsp;&nbsp;&nbsp;&nbsp;";

        private static string CreateOption(SelectListItemTree item, int level, string indentation)
        {
            StringBuilder builder = new StringBuilder();
            TagBuilder builder2 = new TagBuilder("option") {
                InnerHtml = GetIndentation(level, indentation) + HttpUtility.HtmlEncode(item.Text)
            };
            if (item.Value != null)
            {
                builder2.Attributes["Value"] = item.Value;
            }
            if (item.Selected)
            {
                builder2.Attributes["selected"] = "selected";
            }
            builder.AppendLine(builder2.ToString());
            if (item.Items != null)
            {
                level++;
                foreach (SelectListItemTree tree in item.Items)
                {
                    builder.AppendLine(CreateOption(tree, level, indentation));
                }
            }
            return builder.ToString();
        }

        public static IHtmlString DropDownListTree(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItemTree> selectListItemTree)
        {
            return htmlHelper.DropDownListTree(name, selectListItemTree, null, false, null, Indentation);
        }

        public static IHtmlString DropDownListTree(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItemTree> selectListItemTree, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.DropDownListTree(name, selectListItemTree, htmlAttributes, false, null, Indentation);
        }

        public static IHtmlString DropDownListTree(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItemTree> selectListItemTree, string optionLabel)
        {
            return htmlHelper.DropDownListTree(name, selectListItemTree, null, false, optionLabel, Indentation);
        }

        public static IHtmlString DropDownListTree(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItemTree> selectListItemTree, IDictionary<string, object> htmlAttributes, bool allowMutiple, string optionLabel, string indentation)
        {
            ModelState state;
            name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("NULL", "name");
            }
            int level = 0;
            TagBuilder builder = new TagBuilder("select");
            StringBuilder builder2 = new StringBuilder();
            if (optionLabel != null)
            {
                SelectListItemTree item = new SelectListItemTree {
                    Text = optionLabel,
                    Value = string.Empty
                };
                builder2.AppendLine(CreateOption(item, level, indentation));
            }
            foreach (SelectListItemTree tree in selectListItemTree)
            {
                builder2.AppendLine(CreateOption(tree, level, indentation));
            }
            builder.InnerHtml = builder2.ToString();
            builder.MergeAttributes<string, object>(htmlAttributes);
            builder.MergeAttribute("name", name, true);
            builder.GenerateId(name);
            if (allowMutiple)
            {
                builder.MergeAttribute("mutiple", "mutiple");
            }
            if (htmlHelper.ViewData.ModelState.TryGetValue(name, out state) && (state.Errors.Count > 0))
            {
                builder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }
            return new HtmlString(builder.ToString());
        }

        private static string GetIndentation(int level, string indentation)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < level; i++)
            {
                builder.AppendLine(indentation);
            }
            return builder.ToString();
        }

        public static IEnumerable<SelectListItemTree> SetActiveItem(this IEnumerable<SelectListItemTree> listItems, object value)
        {
            string strB = (value == null) ? "" : value.ToString();
            SelectListItemTree[] treeArray = listItems.ToArray<SelectListItemTree>();
            foreach (SelectListItemTree tree in treeArray)
            {
                if (string.Compare(tree.Value, strB, true) == 0)
                {
                    tree.Selected = true;
                }
                else
                {
                    tree.Selected = false;
                }
                if (tree.Items != null)
                {
                    tree.Items.SetActiveItem(value);
                }
            }
            return treeArray;
        }
    }
}

