namespace CMM.Web.Css
{
    using System;
    using System.Runtime.CompilerServices;

    public class SelectorCombinator
    {
        public const char ChildCombinator = '>';
        public const char DescendantCombinator = ' ';
        public const char GeneralSiblingCombinator = '~';
        public const char SiblingCombinator = '+';
        public static readonly char[] ValidCombinators = new char[] { ' ', '>', '+', '~' };

        public SelectorCombinator(char ch)
        {
            this.Value = ch;
        }

        public static implicit operator char(SelectorCombinator combinator)
        {
            return combinator.Value;
        }

        public static implicit operator SelectorCombinator(char ch)
        {
            return new SelectorCombinator(ch);
        }

        public override string ToString()
        {
            return new string(new char[] { this.Value });
        }

        public char Value { get; private set; }
    }
}

