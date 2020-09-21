namespace CMM.Text
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class FormattedTextWriter : TextWriter
    {
        private int _averageLineLength;
        private int _lineLength;
        private int _maxLineLength;
        private char[] _word;
        private int _wordLength;
        private TextWriter _writer;
        public const int AverageWordLength = 5;

        public FormattedTextWriter(TextWriter writer, int lineLength, int overflow = 5)
        {
            if (overflow < 0)
            {
                throw new ArgumentException("Overflow must greater than zero.", "overflow");
            }
            this._writer = writer;
            this._averageLineLength = lineLength;
            this._maxLineLength = this._averageLineLength + overflow;
            this._word = new char[this._averageLineLength];
        }

        private void AppendWord(char ch)
        {
            if (this._wordLength >= this._word.Length)
            {
                this.ExpandWord();
            }
            this._word[this._wordLength] = ch;
            this._wordLength++;
        }

        public override void Close()
        {
            this._writer.Close();
        }

        protected override void Dispose(bool disposing)
        {
            this.Flush();
            this._writer.Dispose();
        }

        private void DoWriteLine()
        {
            this._lineLength = 0;
            this._writer.WriteLine();
        }

        private void ExpandWord()
        {
            char[] array = new char[this._wordLength * 2];
            this._word.CopyTo(array, 0);
            this._word = array;
        }

        public override void Flush()
        {
            this.WriteWord();
            this._writer.Flush();
        }

        private bool IsAppendingWord()
        {
            return (this._wordLength > 0);
        }

        private bool IsSpace(char ch)
        {
            return char.IsWhiteSpace(ch);
        }

        public override void Write(bool value)
        {
            this.Write(value.ToString());
        }

        public override void Write(char value)
        {
            this.Write(value.ToString());
        }

        public override void Write(decimal value)
        {
            this.Write(value.ToString());
        }

        public override void Write(double value)
        {
            this.Write(value.ToString());
        }

        public override void Write(int value)
        {
            this.Write(value.ToString());
        }

        public override void Write(long value)
        {
            this.Write(value.ToString());
        }

        public override void Write(char[] buffer)
        {
            this.Write(new string(buffer));
        }

        public override void Write(object value)
        {
            this.Write(value.ToString());
        }

        public override void Write(float value)
        {
            this.Write(value.ToString());
        }

        public override void Write(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                char ch = value[i];
                if (this.IsSpace(ch))
                {
                    if (this.IsAppendingWord())
                    {
                        this.WriteWord();
                    }
                    this.WriteSpace(ch);
                }
                else
                {
                    this.AppendWord(ch);
                }
            }
        }

        public override void Write(uint value)
        {
            this.Write(value.ToString());
        }

        public override void Write(ulong value)
        {
            this.Write(value.ToString());
        }

        public override void Write(string format, object arg0)
        {
            this.Write(string.Format(format, arg0));
        }

        public override void Write(string format, params object[] arg)
        {
            this.Write(string.Format(format, arg));
        }

        public override void Write(char[] buffer, int index, int count)
        {
            this.Write(new string(buffer, index, count));
        }

        public override void Write(string format, object arg0, object arg1)
        {
            this.Write(string.Format(format, arg0, arg1));
        }

        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            this.Write(string.Format(format, arg0, arg1, arg2));
        }

        public override void WriteLine()
        {
            this.WriteWord();
            this.DoWriteLine();
        }

        public override void WriteLine(bool value)
        {
            this.Write(value.ToString());
            this.WriteLine();
        }

        public override void WriteLine(char value)
        {
            this.Write(value.ToString());
            this.WriteLine();
        }

        public override void WriteLine(decimal value)
        {
            this.Write(value.ToString());
            this.WriteLine();
        }

        public override void WriteLine(double value)
        {
            this.Write(value.ToString());
            this.WriteLine();
        }

        public override void WriteLine(char[] buffer)
        {
            this.Write(new string(buffer));
            this.WriteLine();
        }

        public override void WriteLine(int value)
        {
            this.Write(value.ToString());
            this.WriteLine();
        }

        public override void WriteLine(long value)
        {
            this.Write(value.ToString());
            this.WriteLine();
        }

        public override void WriteLine(object value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(float value)
        {
            this.Write(value.ToString());
            this.WriteLine();
        }

        public override void WriteLine(string value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(uint value)
        {
            this.Write(value.ToString());
            this.WriteLine();
        }

        public override void WriteLine(ulong value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(string format, params object[] arg)
        {
            this.Write(string.Format(format, arg));
            this.WriteLine();
        }

        public override void WriteLine(string format, object arg0)
        {
            this.Write(string.Format(format, arg0));
            this.WriteLine();
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            this.Write(new string(buffer, index, count));
            this.WriteLine();
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            this.Write(string.Format(format, arg0, arg1));
            this.WriteLine();
        }

        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            this.Write(string.Format(format, arg0, arg1, arg2));
            this.WriteLine();
        }

        private void WriteSpace(char ch)
        {
            this._writer.Write(ch);
            this._lineLength++;
        }

        private void WriteWord()
        {
            if (this._wordLength != 0)
            {
                if (this._lineLength >= this._averageLineLength)
                {
                    this.DoWriteLine();
                }
                if ((this._lineLength > 0) && ((this._lineLength + this._wordLength) > this._maxLineLength))
                {
                    this.DoWriteLine();
                }
                this._writer.Write(this._word, 0, this._wordLength);
                this._lineLength += this._wordLength;
                this._wordLength = 0;
            }
        }

        public override System.Text.Encoding Encoding
        {
            get
            {
                return this._writer.Encoding;
            }
        }

        public override IFormatProvider FormatProvider
        {
            get
            {
                return this._writer.FormatProvider;
            }
        }

        public override string NewLine
        {
            get
            {
                return this._writer.NewLine;
            }
            set
            {
                base.NewLine = value;
            }
        }
    }
}

