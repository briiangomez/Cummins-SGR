namespace CMM.Web.Css
{
    using System;
    using System.IO;

    public class StyleSheetReader
    {
        private int _p;
        private ReadStatus _status;
        private string _str;

        public StyleSheetReader(Stream stream)
        {
            this._status = ReadStatus.RuleSet;
            using (StreamReader reader = new StreamReader(stream))
            {
                this.Initialize(reader);
            }
        }

        public StyleSheetReader(TextReader reader)
        {
            this._status = ReadStatus.RuleSet;
            this.Initialize(reader);
        }

        public StyleSheetReader(string str)
        {
            this._status = ReadStatus.RuleSet;
            this.Initialize(str);
        }

        private void Initialize(TextReader reader)
        {
            this._str = reader.ReadToEnd();
        }

        private void Initialize(string str)
        {
            this._str = str;
            this.SkipToNextStatement();
        }

        private bool IsWhiteSpace(char ch)
        {
            return char.IsWhiteSpace(ch);
        }

        public string ReadAtRule()
        {
            if (this._status != ReadStatus.AtRule)
            {
                throw new InvalidReadingCallException("The function can only be called when reading status in AtRule.");
            }
            int startIndex = this._p;
            int num2 = 0;
            while (!this.EndOfStream)
            {
                char ch = this._str[this._p];
                if (ch == '{')
                {
                    num2++;
                }
                else if ((ch == '}') && (num2 > 1))
                {
                    num2--;
                }
                else if (((ch == '}') && (num2 == 1)) || ((ch == ';') && (num2 == 0)))
                {
                    this._p++;
                    string str = this._str.Substring(startIndex, this._p - startIndex);
                    this.SkipToNextStatement();
                    return str;
                }
                this._p++;
            }
            return this._str.Substring(startIndex);
        }

        public string ReadRuleSet()
        {
            if (this._status != ReadStatus.RuleSet)
            {
                throw new InvalidReadingCallException("The function can only be called when reading status in RuleSet.");
            }
            int startIndex = this._p;
            while (!this.EndOfStream)
            {
                char ch = this._str[this._p];
                if (ch == '}')
                {
                    this._p++;
                    string str = this._str.Substring(startIndex, this._p - startIndex);
                    this.SkipToNextStatement();
                    return str;
                }
                this._p++;
            }
            return this._str.Substring(startIndex);
        }

        private void SkipToNextStatement()
        {
            this.SkipWhiteSpace();
            if (!this.EndOfStream)
            {
                if (this._str[this._p] == '@')
                {
                    this._status = ReadStatus.AtRule;
                }
                else
                {
                    this._status = ReadStatus.RuleSet;
                }
            }
        }

        private void SkipWhiteSpace()
        {
            while (!this.EndOfStream)
            {
                char ch = this._str[this._p];
                if (!this.IsWhiteSpace(ch))
                {
                    break;
                }
                this._p++;
            }
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
            AtRule,
            RuleSet,
            EndOfStream
        }
    }
}

