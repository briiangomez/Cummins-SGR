namespace CMM.Web.Css
{
    using System;
    using System.Linq;

    public class SelectorReader
    {
        private int _p;
        private ReadStatus _status;
        private string _str;
        private static readonly char[] SimpleSelectorSeparators = new char[] { "#"[0], "."[0], '[', ":"[0], "::"[0] };

        public SelectorReader(string str)
        {
            this._str = str.Trim();
            this._status = ReadStatus.SimpleSelector;
        }

        private bool IsCombinator(char ch)
        {
            return SelectorCombinator.ValidCombinators.Contains<char>(ch);
        }

        private bool IsSimpleSelectorSeparator(char ch)
        {
            return SimpleSelectorSeparators.Contains<char>(ch);
        }

        private bool IsWhiteSpace(char ch)
        {
            return char.IsWhiteSpace(ch);
        }

        public char ReadCombinator()
        {
            if (this._status != ReadStatus.Combinator)
            {
                throw new InvalidReadingCallException("The function can only be called when reading status in Combinator.");
            }
            int startIndex = this._p;
            while (!this.EndOfStream)
            {
                char ch = this._str[this._p];
                if (!(this.IsCombinator(ch) || this.IsWhiteSpace(ch)))
                {
                    this._status = ReadStatus.SimpleSelector;
                    break;
                }
                this._p++;
            }
            if (this.EndOfStream)
            {
                return '\0';
            }
            string str = this._str.Substring(startIndex, this._p - startIndex);
            if (str.Length > 0)
            {
                str = str.Trim();
                if (str.Length == 0)
                {
                    str = " ";
                }
            }
            if (str.Length != 1)
            {
                throw new InvalidStructureException(string.Format("Invalid combinator reading in sub string {0}.", this._str.Substring(startIndex)));
            }
            return str[0];
        }

        public string ReadSimpleSelector()
        {
            if (this._status != ReadStatus.SimpleSelector)
            {
                throw new InvalidReadingCallException("The function can only be called when reading status in SimpleSelector.");
            }
            int startIndex = this._p;
            while (!this.EndOfStream)
            {
                char ch = this._str[this._p];
                if ((this._p > startIndex) && this.IsSimpleSelectorSeparator(ch))
                {
                    break;
                }
                if (this.IsCombinator(ch))
                {
                    this._status = ReadStatus.Combinator;
                    break;
                }
                this._p++;
            }
            return this._str.Substring(startIndex, this._p - startIndex);
        }

        public bool EndOfStream
        {
            get
            {
                if (this._status != ReadStatus.EndOfStream)
                {
                    if (this._p < this._str.Length)
                    {
                        return false;
                    }
                    this._status = ReadStatus.EndOfStream;
                }
                return true;
            }
        }

        public ReadStatus Status
        {
            get
            {
                return this._status;
            }
        }

        public enum ReadStatus
        {
            SimpleSelector,
            Combinator,
            EndOfStream
        }
    }
}

