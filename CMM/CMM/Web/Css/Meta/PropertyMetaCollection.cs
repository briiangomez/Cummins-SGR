namespace CMM.Web.Css.Meta
{
    using System;
    using System.Collections.Generic;

    public class PropertyMetaCollection : Dictionary<string, PropertyMeta>
    {
        public void Add(PropertyMeta meta)
        {
            base.Add(meta.Name, meta);
            if (meta.IsShorthand)
            {
                foreach (string str in meta.SubProperties)
                {
                    if (base.ContainsKey(str))
                    {
                        PropertyMeta meta2 = base[str];
                        meta2.ShorthandName = meta.Name;
                    }
                }
            }
        }
    }
}

