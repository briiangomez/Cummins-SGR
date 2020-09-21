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
    using System.Text;
    using System.Threading;

    public class ValueDiscriminationRule : ShorthandRule
    {
        private bool _groupable = false;
        private List<SubList> _list = new List<SubList>();

        public ValueDiscriminationRule(string grammar)
        {
            this._list = this.CreateList(grammar);
            this._groupable = this._list.Any<SubList>(o => o.Type == SubListType.Sequencial);
        }

        protected List<SubList> CreateList(string grammar)
        {
            List<SubList> list = new List<SubList>();
            string[] strArray = grammar.Split(new char[] { ']' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strArray)
            {
                if (str.Contains<char>('['))
                {
                    list.Add(new SubList(str.TrimStart(new char[] { '[' }).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), SubListType.Alternative));
                }
                else
                {
                    foreach (string str2 in str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (str2.Contains<char>('/'))
                        {
                            list.Add(new SubList(str2.TrimStart(new char[] { ' ' }).Split(new char[] { '/' }), SubListType.Sequencial));
                        }
                        else
                        {
                            list.Add(new SubList(str2));
                        }
                    }
                }
            }
            return list;
        }

        protected virtual List<SubList> CreateMatchList(List<SubList> list)
        {
            List<SubList> list2 = new List<SubList>();
            foreach (SubList list3 in list)
            {
                if (list3.Type == SubListType.Alternative)
                {
                    list2.Add(new SubList(list3, list3.Type));
                }
                else
                {
                    list2.Add(list3);
                }
            }
            return list2;
        }

        protected override IEnumerable<Property> Split(List<string> splitted, PropertyMeta meta)
        {
            return this.Split(splitted, meta, this.CreateMatchList(this._list));
        }

        protected virtual IEnumerable<Property> Split(List<string> splitted, PropertyMeta meta, List<SubList> matchList)
        {
            foreach (string iteratorVariable0 in splitted)
            {
                if (this._groupable && iteratorVariable0.Contains<char>('/'))
                {
                    string[] iteratorVariable1 = iteratorVariable0.Split(new char[] { '/' });
                    for (int j = 0; j < matchList.Count; j++)
                    {
                        SubList source = matchList[j];
                        if ((matchList[j].Type == SubListType.Sequencial) && (matchList[j].Count<string>() >= iteratorVariable1.Length))
                        {
                            PropertyMeta iteratorVariable4 = PropertyMeta.GetMeta(source.First<string>());
                            if ((iteratorVariable4 != null) && iteratorVariable4.ValueType.IsValid(iteratorVariable1[0]))
                            {
                                List<string>.Enumerator enumerator = source.GetEnumerator();
                                IEnumerator iteratorVariable6 = iteratorVariable1.GetEnumerator();
                                while (iteratorVariable6.MoveNext() && enumerator.MoveNext())
                                {
                                    yield return new Property(enumerator.Current, iteratorVariable6.Current.ToString());
                                }
                                matchList.RemoveAt(j);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < matchList.Count; k++)
                    {
                        SubList iteratorVariable8 = matchList[k];
                        if (iteratorVariable8.Type == SubListType.Alternative)
                        {
                            Property iteratorVariable9 = null;
                            for (int m = 0; m < iteratorVariable8.Count; m++)
                            {
                                PropertyMeta subMeta = PropertyMeta.GetMeta(iteratorVariable8[m]);
                                if ((subMeta != null) && subMeta.ValueType.IsValid(iteratorVariable0))
                                {
                                    iteratorVariable9 = new Property(iteratorVariable8[m], iteratorVariable0);
                                    iteratorVariable8.RemoveAt(m);
                                    break;
                                }
                            }
                            if (iteratorVariable9 == null)
                            {
                                goto Label_03BC;
                            }
                            yield return iteratorVariable9;
                            break;
                        }
                        PropertyMeta iteratorVariable10 = PropertyMeta.GetMeta(iteratorVariable8.First<string>());
                        if ((iteratorVariable10 != null) && iteratorVariable10.ValueType.IsValid(iteratorVariable0))
                        {
                            yield return new Property(iteratorVariable8.First<string>(), iteratorVariable0);
                            matchList.RemoveAt(k);
                            break;
                        }
                    Label_03BC:;
                    }
                }
            }
        }

        public override IEnumerable<string> SubProperties(PropertyMeta meta)
        {
            return _list.SelectMany(o => o);
        }

        public override bool TryCombine(IEnumerable<Property> properties, PropertyMeta meta, out Property property)
        {
            StringBuilder builder = new StringBuilder();
            using (List<SubList>.Enumerator enumerator = this._list.GetEnumerator())
            {
                Func<Property, bool> func2 = null;
                SubList propertyNames;
                while (enumerator.MoveNext())
                {
                    propertyNames = enumerator.Current;
                    Func<Property, bool> predicate = null;
                    if ((propertyNames.Type == SubListType.Sequencial) || (propertyNames.Type == SubListType.MustContains))
                    {
                    }
                    if (!((func2 != null) || properties.Any<Property>((func2 = o => o.Name == propertyNames.First<string>()))))
                    {
                        property = null;
                        return false;
                    }
                    List<string>.Enumerator propertyName = propertyNames.GetEnumerator();
                    int num = 0;
                    while (propertyName.MoveNext())
                    {
                        if (predicate == null)
                        {
                            predicate = o => o.Name == propertyName.Current;
                        }
                        Property property2 = properties.FirstOrDefault<Property>(predicate);
                        if (property2 != null)
                        {
                            if (num == 0)
                            {
                                if (builder.Length > 0)
                                {
                                    builder.Append(' ');
                                }
                            }
                            else if (propertyNames.Type == SubListType.Sequencial)
                            {
                                builder.Append('/');
                            }
                            else
                            {
                                builder.Append(' ');
                            }
                            builder.Append(property2.Value);
                            num++;
                        }
                    }
                }
            }
            property = new Property(meta.Name, builder.ToString());
            return true;
        }

        protected IList<SubList> Grammar
        {
            get
            {
                return this._list;
            }
        }


        protected class SubList : List<string>
        {
            public SubList(string value) : this(new string[] { value }, ValueDiscriminationRule.SubListType.MustContains)
            {
            }

            public SubList(IEnumerable<string> list, ValueDiscriminationRule.SubListType type)
            {
                base.AddRange(list);
                this.Type = type;
            }

            public ValueDiscriminationRule.SubListType Type { get; private set; }
        }

        protected enum SubListType
        {
            Alternative,
            Sequencial,
            MustContains
        }
    }
}

