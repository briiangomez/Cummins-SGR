namespace CMM.Web.Css.Meta
{
    using CMM.Web.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class BackgroundShorthandRule : ValueDiscriminationRule
    {
        private List<SubList> _splitGrammar;

        public BackgroundShorthandRule()
            : base("[background-color background-image background-repeat background-attachment background-position]")
        {
            _splitGrammar = CreateList("[background-color background-image background-repeat background-attachment background-position-left background-position-top]");
        }

        protected override IEnumerable<Property> Split(List<string> splitted, PropertyMeta meta)
        {
            Property left = null, top = null;
            foreach (var each in base.Split(splitted, meta, CreateMatchList(_splitGrammar)))
            {
                switch (each.Name)
                {
                    case "background-position-left":
                        left = each;
                        break;
                    case "background-position-top":
                        top = each;
                        break;
                    default:
                        yield return each;
                        break;
                }

            }

            if (left != null || top != null)
            {
                StringBuilder builder = new StringBuilder();
                if (left != null)
                {
                    builder.Append(left.Value);
                }
                if (top != null)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" ");
                    }
                    builder.Append(top.Value);
                }

                yield return new Property("background-position", builder.ToString());
            }
        }

        public override bool TryCombine(IEnumerable<Property> properties, PropertyMeta meta, out Property property)
        {
            // We do not combine background for background-image not supported by email client.
            property = null;
            return false;
        }
    }
}

