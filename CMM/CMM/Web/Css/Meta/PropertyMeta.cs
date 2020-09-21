namespace CMM.Web.Css.Meta
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PropertyMeta
    {
        private static PropertyMetaCollection _metas = new PropertyMetaCollection();
        private IEnumerable<string> _subProperties;

        static PropertyMeta()
        {
            _metas.Add(new PropertyMeta("font-style", new EnumType("normal | italic | oblique")));
            _metas.Add(new PropertyMeta("font-variant", new EnumType("normal | small-caps")));
            _metas.Add(new PropertyMeta("font-weight", new EnumType("normal | bold | bolder | lighter | 100 | 200 | 300 | 400 | 500 | 600 | 700 | 800 | 900")));
            _metas.Add(new PropertyMeta("font-size", PropertyValueType.Size));
            _metas.Add(new PropertyMeta("line-height", PropertyValueType.Size));
            _metas.Add(new PropertyMeta("font-family", new FontFamilyType()));
            _metas.Add(new PropertyMeta("font", new ShorthandType(new FontShorthandRule())));
            foreach (string str in PositionShorthandRule.Positions)
            {
                foreach (KeyValuePair<string, PropertyValueType> pair in BorderPositionShorthandRule.BorderProperties)
                {
                    _metas.Add(new PropertyMeta("border-" + str + "-" + pair.Key, pair.Value));
                }
            }
            foreach (KeyValuePair<string, PropertyValueType> pair in BorderPositionShorthandRule.BorderProperties)
            {
                _metas.Add(new PropertyMeta("border-" + pair.Key, new ShorthandType(ShorthandRule.BorderProperty, pair.Value)));
            }
            Func<KeyValuePair<string, PropertyValueType>, string> selector = null;
            foreach (string position in PositionShorthandRule.Positions)
            {
                if (selector == null)
                {
                    selector = o => "border-" + position + "-" + o.Key;
                }
                string grammar = "[" + string.Join(" ", BorderPositionShorthandRule.BorderProperties.Select<KeyValuePair<string, PropertyValueType>, string>(selector)) + "]";
                _metas.Add(new PropertyMeta("border-" + position, new ShorthandType(new ValueDiscriminationRule(grammar))));
            }
            _metas.Add(new PropertyMeta("border", new ShorthandType(new ValueDiscriminationRule(string.Join(" ", (IEnumerable<string>) (from o in BorderPositionShorthandRule.BorderProperties select "border-" + o.Key))))));
            _metas.Add(new PropertyMeta("list-style-type", new EnumType("disc | circle | square | decimal | decimal-leading-zero | lower-roman | upper-roman | lower-greek | lower-latin | upper-latin | armenian | georgian | lower-alpha | upper-alpha | none")));
            _metas.Add(new PropertyMeta("list-style-image", PropertyValueType.Url));
            _metas.Add(new PropertyMeta("list-style-position", new EnumType("inside | outside")));
            _metas.Add(new PropertyMeta("list-style", new ShorthandType(new ValueDiscriminationRule("[list-style-type list-style-image list-style-position]"))));
            _metas.Add(new PropertyMeta("background-color", PropertyValueType.Color));
            _metas.Add(new PropertyMeta("background-image", PropertyValueType.Url));
            _metas.Add(new PropertyMeta("background-repeat", new EnumType("repeat | repeat-x | repeat-y | no-repeat")));
            _metas.Add(new PropertyMeta("background-attachment", new EnumType("scroll | fixed")));
            _metas.Add(new PropertyMeta("background-position", PropertyValueType.Any));
            _metas.Add(new PropertyMeta("background-position-left", new CombinedType(new PropertyValueType[] { new EnumType("left | center | right"), PropertyValueType.Length })));
            _metas.Add(new PropertyMeta("background-position-top", new CombinedType(new PropertyValueType[] { new EnumType("top | center | bottom"), PropertyValueType.Length })));
            _metas.Add(new PropertyMeta("background", new ShorthandType(new BackgroundShorthandRule())));
            foreach (string str3 in PositionShorthandRule.Positions)
            {
                _metas.Add(new PropertyMeta("margin-" + str3, PropertyValueType.Length));
            }
            _metas.Add(new PropertyMeta("margin", new ShorthandType(ShorthandRule.Margin)));
            foreach (string str3 in PositionShorthandRule.Positions)
            {
                _metas.Add(new PropertyMeta("padding-" + str3, PropertyValueType.Length));
            }
            _metas.Add(new PropertyMeta("padding", new ShorthandType(ShorthandRule.Margin)));
        }

        public PropertyMeta(string name, PropertyValueType valueType)
        {
            this.Name = name;
            this.ValueType = valueType;
        }

        public static PropertyMeta GetMeta(string name)
        {
            if (_metas.ContainsKey(name))
            {
                return _metas[name];
            }
            return null;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public bool IsShorthand
        {
            get
            {
                return (this.ValueType is ShorthandType);
            }
        }

        public string Name { get; private set; }

        public string ShorthandName { get; internal set; }

        public IEnumerable<string> SubProperties
        {
            get
            {
                if (!this.IsShorthand)
                {
                    throw new InvalidOperationException("Property can only be accessed in case of shorthand, please use IsShorthand first.");
                }
                if (this._subProperties == null)
                {
                    this._subProperties = (this.ValueType as ShorthandType).ShorthandRule.SubProperties(this);
                }
                return this._subProperties;
            }
        }

        public PropertyValueType ValueType { get; private set; }
    }
}

