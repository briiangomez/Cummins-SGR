namespace CMM.Web.Html
{
    using System;
    using System.Linq;

    public class HtmlReader
    {
        private string _html;
        private int _p;
        private ReadStatus _status;
        public static readonly string[] AbbreviatableTags = new string[] { "base", "link", "img", "br", "hr" };

        public HtmlReader(string html)
        {
            this._html = html;
            this._status = ReadStatus.Text;
            this.MoveToNextElement();
        }

        private bool IsAttributeBreak()
        {
            return (char.IsWhiteSpace(this._html[this._p]) || this.IsTagEnd());
        }

        private bool IsAttributeNameValueSeparator()
        {
            return (this._html[this._p] == '=');
        }

        private bool IsAttributeQuote()
        {
            return ((this._html[this._p] == '"') || (this._html[this._p] == '\''));
        }

        private bool IsEndTagStart()
        {
            return (this._html.Substring(this._p, 2) == "</");
        }

        private bool IsSpace()
        {
            return char.IsWhiteSpace(this._html[this._p]);
        }

        public bool IsStartTag()
        {
            this.MoveToTagStart();
            return !this.IsEndTagStart();
        }

        private bool IsStartTagEnd()
        {
            if (this._p == (this._html.Length - 1))
            {
                return false;
            }
            return (this._html.Substring(this._p, 2) == "/>");
        }

        public bool IsTag()
        {
            return (this._status == ReadStatus.Tag);
        }

        private bool IsTagEnd()
        {
            return ((this._html[this._p] == '>') || this.IsStartTagEnd());
        }

        private bool IsTagStart()
        {
            return (this._html[this._p] == '<');
        }

        public bool MoveToAttribute(string attributeName)
        {
            this.MoveToTagStart();
            this.MoveToFirstAttribute();
            while (!this.IsTagEnd())
            {
                if (string.Compare(this.ReadAttributeName(), attributeName, true) == 0)
                {
                    return true;
                }
                this.MoveToNextAttribute();
            }
            return false;
        }

        public bool MoveToFirstAttribute()
        {
            this.MoveToTagStart();
            return this.MoveToNextAttribute();
        }

        public bool MoveToNextAttribute()
        {
            while (!this.IsAttributeBreak())
            {
                if (this.IsTagEnd())
                {
                    return false;
                }
                this._p++;
            }
            this.SkipSpace();
            if (this.IsTagEnd())
            {
                return false;
            }
            return true;
        }

        public bool MoveToNextElement()
        {
            if (this.EndOfReading)
            {
                return false;
            }
            if (this._status != ReadStatus.Tag)
            {
                while (!this.EndOfReading && !this.IsTagStart())
                {
                    this._p++;
                }
                if (this.EndOfReading)
                {
                    return false;
                }
                this._status = ReadStatus.Tag;
            }
            else
            {
                this.MoveToTagEnd();
                this.SkipTagEnd();
                if (this.EndOfReading)
                {
                    return false;
                }
                this._status = this.IsTagStart() ? ReadStatus.Tag : ReadStatus.Text;
            }
            return true;
        }

        public void MoveToNextNode()
        {
            if (this.IsTag() && !this.IsEndTagStart())
            {
                string str = this.ReadTagName();
                if (AbbreviatableTags.Contains<string>(str))
                {
                    this.MoveToNextElement();
                }
                else
                {
                    this.MoveToTagEnd();
                    if (this.IsStartTagEnd())
                    {
                        this.MoveToNextElement();
                    }
                    else
                    {
                        this.MoveToNextElement();
                        int num = 1;
                        while (!this.EndOfReading && (num > 0))
                        {
                            if (this.IsTag())
                            {
                                if (this.IsEndTagStart())
                                {
                                    num--;
                                    if (num == 0)
                                    {
                                        string strB = this.ReadTagName();
                                        if (string.Compare(str, strB, true) != 0)
                                        {
                                            throw new InvalidHtmlException(string.Format("Html is invalid, start tag {0} and end tag {1} are not matched", str, strB));
                                        }
                                    }
                                }
                                else
                                {
                                    string str3 = this.ReadTagName();
                                    this.MoveToTagEnd();
                                    if (!this.IsStartTagEnd() && !AbbreviatableTags.Contains<string>(str3))
                                    {
                                        num++;
                                    }
                                }
                            }
                            this.MoveToNextElement();
                        }
                    }
                }
            }
        }

        private void MoveToTagEnd()
        {
            while (!this.IsTagEnd())
            {
                this._p++;
            }
        }

        public void MoveToTagStart()
        {
            while (!this.IsTagStart())
            {
                this._p--;
            }
        }

        public string ReadAttributeName()
        {
            int startIndex = this._p;
            while (!this.IsAttributeNameValueSeparator() && !this.IsAttributeBreak())
            {
                this._p++;
            }
            return this._html.Substring(startIndex, this._p - startIndex);
        }

        public string ReadAttributeValue()
        {
            if (this.IsAttributeNameValueSeparator())
            {
                this._p++;
            }
            while (this.IsSpace())
            {
                this._p++;
            }
            char ch = '\0';
            if (this.IsAttributeQuote())
            {
                ch = this._html[this._p];
                this._p++;
            }
            else
            {
                return null;
            }
            int startIndex = this._p;
            while (this._html[this._p] != ch)
            {
                this._p++;
            }
            return this._html.Substring(startIndex, this._p - startIndex);
        }

        public string ReadNode()
        {
            this.MoveToTagStart();
            int startIndex = this._p;
            this.MoveToNextNode();
            return this._html.Substring(startIndex, this._p - startIndex);
        }

        public string ReadTag()
        {
            this.MoveToTagStart();
            int startIndex = this._p;
            this.MoveToNextElement();
            return this._html.Substring(startIndex, this._p - startIndex);
        }

        public string ReadTagName()
        {
            this.MoveToTagStart();
            this.SkipTagStart();
            int startIndex = this._p;
            while (!this.IsAttributeBreak())
            {
                this._p++;
            }
            return this._html.Substring(startIndex, this._p - startIndex);
        }

        public string ReadText()
        {
            int startIndex = this._p;
            this.MoveToNextElement();
            return this._html.Substring(startIndex, this._p - startIndex);
        }

        private void SkipSpace()
        {
            while (this.IsSpace())
            {
                this._p++;
            }
        }

        private void SkipTagEnd()
        {
            if (this.IsStartTagEnd())
            {
                this._p += 2;
            }
            else
            {
                this._p++;
            }
        }

        private void SkipTagStart()
        {
            if (this.IsEndTagStart())
            {
                this._p += 2;
            }
            else
            {
                this._p++;
            }
        }

        public bool EndOfReading
        {
            get
            {
                return (this._p >= this._html.Length);
            }
        }

        public int Position
        {
            get
            {
                return this._p;
            }
        }

        internal enum ReadStatus
        {
            Text,
            Tag
        }
    }
}

