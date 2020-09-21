namespace CMM.Web.Css.Meta
{
    using CMM.Web.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class PositionDescriminationRule : ShorthandRule
    {
        private Dictionary<int, string[]> _mappings = new Dictionary<int, string[]>();
        private string[] _subProperties = null;

        public PositionDescriminationRule(params string[] grammars)
        {
            foreach (string str in grammars)
            {
                this.AddMapping(str);
            }
            this._subProperties = (from o in this._mappings
                orderby o.Key descending
                select o).First<KeyValuePair<int, string[]>>().Value;
        }

        public void AddMapping(string grammar)
        {
            string[] strArray = grammar.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            this._mappings.Add(strArray.Length, strArray);
        }

        protected virtual string CombineName(PropertyMeta meta, string name)
        {
            return (meta.Name + "-" + name);
        }

        private IEnumerable<Tuple<Property, int>> Filter(IEnumerable<Property> properties, PropertyMeta meta)
        {
            string[] iteratorVariable0 = (from o in this._mappings
                orderby o.Key descending
                select o).First<KeyValuePair<int, string[]>>().Value;
            foreach (Property iteratorVariable1 in properties)
            {
                string iteratorVariable2 = iteratorVariable1.Name.Substring(meta.Name.Length + 1);
                int index = 0;
                while ((index < iteratorVariable0.Length) && (iteratorVariable0[index] != iteratorVariable2))
                {
                    index++;
                }
                if (index < iteratorVariable0.Length)
                {
                    yield return new Tuple<Property, int>(iteratorVariable1, index);
                }
            }
        }

        protected override IEnumerable<Property> Split(List<string> splitted, PropertyMeta meta)
        {
            for (int i = 0; i < splitted.Count; i++)
            {
                if (this._mappings.ContainsKey(splitted.Count))
                {
                    foreach (string iteratorVariable1 in this._mappings[splitted.Count][i].Split(new char[] { ',' }))
                    {
                        yield return new Property(this.CombineName(meta, iteratorVariable1), splitted[i]);
                    }
                }
            }
        }

        public override IEnumerable<string> SubProperties(PropertyMeta meta)
        {
            return (from o in this._subProperties select this.CombineName(meta, o));
        }

        public override bool TryCombine(IEnumerable<Property> properties, PropertyMeta meta, out Property property)
        {
            if (this.SubProperties(meta).Except<string>((from o in properties select o.Name)).Count<string>() > 0)
            {
                property = null;
                return false;
            }
            string str = string.Join(" ", (IEnumerable<string>) (from o in this.Filter(properties, meta)
                orderby o.Item2
                select o.Item1.Value));
            property = new Property(meta.Name, str);
            return true;
        }


    }
}

