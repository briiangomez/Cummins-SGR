namespace CMM.Web.Script
{
    using System;
    using System.IO;

    public class JSMin
    {
        private const int EOF = -1;
        private int theA;
        private int theB;
        private int theLookahead = -1;
        private TextReader tr;
        private TextWriter tw;

        private void action(int d)
        {
            bool flag;
            if (d <= 1)
            {
                this.put(this.theA);
            }
            if (d <= 2)
            {
                this.theA = this.theB;
                if ((this.theA == 0x27) || (this.theA == 0x22))
                {
                    while (true)
                    {
                        flag = true;
                        this.put(this.theA);
                        this.theA = this.get();
                        if (this.theA == this.theB)
                        {
                            break;
                        }
                        if (this.theA <= 10)
                        {
                            throw new Exception(string.Format("Error: JSMIN unterminated string literal: {0}\n", this.theA));
                        }
                        if (this.theA == 0x5c)
                        {
                            this.put(this.theA);
                            this.theA = this.get();
                        }
                    }
                }
            }
            if (d <= 3)
            {
                this.theB = this.next();
                if ((this.theB == 0x2f) && ((((((this.theA == 40) || (this.theA == 0x2c)) || ((this.theA == 0x3d) || (this.theA == 0x5b))) || (((this.theA == 0x21) || (this.theA == 0x3a)) || ((this.theA == 0x26) || (this.theA == 0x7c)))) || (((this.theA == 0x3f) || (this.theA == 0x7b)) || ((this.theA == 0x7d) || (this.theA == 0x3b)))) || (this.theA == 10)))
                {
                    this.put(this.theA);
                    this.put(this.theB);
                    while (true)
                    {
                        flag = true;
                        this.theA = this.get();
                        if (this.theA == 0x2f)
                        {
                            this.theB = this.next();
                            return;
                        }
                        if (this.theA == 0x5c)
                        {
                            this.put(this.theA);
                            this.theA = this.get();
                        }
                        else if (this.theA <= 10)
                        {
                            throw new Exception(string.Format("Error: JSMIN unterminated Regular Expression literal : {0}.\n", this.theA));
                        }
                        this.put(this.theA);
                    }
                }
            }
        }

        private int get()
        {
            int theLookahead = this.theLookahead;
            this.theLookahead = -1;
            if (theLookahead == -1)
            {
                theLookahead = this.tr.Read();
            }
            if (((theLookahead >= 0x20) || (theLookahead == 10)) || (theLookahead == -1))
            {
                return theLookahead;
            }
            if (theLookahead == 13)
            {
                return 10;
            }
            return 0x20;
        }

        private bool isAlphanum(int c)
        {
            return ((((((c >= 0x61) && (c <= 0x7a)) || ((c >= 0x30) && (c <= 0x39))) || (((c >= 0x41) && (c <= 90)) || ((c == 0x5f) || (c == 0x24)))) || (c == 0x5c)) || (c > 0x7e));
        }

        private void jsmin()
        {
            this.theA = 10;
            this.action(3);
            while (this.theA != -1)
            {
                int theA = this.theA;
                if (theA == 10)
                {
                    theA = this.theB;
                    switch (theA)
                    {
                        case 0x2b:
                        case 0x2d:
                        case 0x5b:
                        case 0x7b:
                        case 40:
                            goto Label_0096;

                        case 0x2c:
                            goto Label_00AC;

                        case 0x20:
                            goto Label_00A1;
                    }
                    goto Label_00AC;
                }
                if (theA != 0x20)
                {
                    goto Label_00DD;
                }
                if (this.isAlphanum(this.theB))
                {
                    this.action(1);
                }
                else
                {
                    this.action(2);
                }
                goto Label_01AE;
            Label_0096:
                this.action(1);
                goto Label_01AE;
            Label_00A1:
                this.action(3);
                goto Label_01AE;
            Label_00AC:
                if (this.isAlphanum(this.theB))
                {
                    this.action(1);
                }
                else
                {
                    this.action(2);
                }
                goto Label_01AE;
            Label_00DD:
                theA = this.theB;
                if (theA == 10)
                {
                    theA = this.theA;
                    switch (theA)
                    {
                        case 0x5d:
                        case 0x7d:
                        case 0x27:
                        case 0x29:
                        case 0x2b:
                        case 0x2d:
                        case 0x22:
                            goto Label_0168;

                        case 40:
                        case 0x2a:
                        case 0x2c:
                            goto Label_0173;
                    }
                    goto Label_0173;
                }
                if (theA != 0x20)
                {
                    goto Label_01A1;
                }
                if (this.isAlphanum(this.theA))
                {
                    this.action(1);
                }
                else
                {
                    this.action(3);
                }
                goto Label_01AE;
            Label_0168:
                this.action(1);
                goto Label_01AE;
            Label_0173:
                if (this.isAlphanum(this.theA))
                {
                    this.action(1);
                }
                else
                {
                    this.action(3);
                }
                goto Label_01AE;
            Label_01A1:
                this.action(1);
            Label_01AE:;
            }
        }

        public static string Minify(string js)
        {
            return new JSMin().MinifyInternal(js);
        }

        public static void Minify(string srcFile, string destFile)
        {
            new JSMin().MinifyInternal(srcFile, destFile);
        }

        private string MinifyInternal(string js)
        {
            string str;
            using (this.tr = new StringReader(js))
            {
                using (this.tw = new StringWriter())
                {
                    this.jsmin();
                    str = this.tw.ToString();
                }
            }
            return str;
        }

        private void MinifyInternal(string src, string dst)
        {
            using (this.tr = new StreamReader(src))
            {
                using (this.tw = new StreamWriter(dst))
                {
                    this.jsmin();
                }
            }
        }

        private int next()
        {
            bool flag;
            int num = this.get();
            if (num != 0x2f)
            {
                return num;
            }
            int num3 = this.peek();
            if (num3 == 0x2a)
            {
                this.get();
            }
            else
            {
                if (num3 != 0x2f)
                {
                    return num;
                }
                while (true)
                {
                    flag = true;
                    num = this.get();
                    if (num <= 10)
                    {
                        return num;
                    }
                }
            }
            while (true)
            {
                flag = true;
                num3 = this.get();
                if (num3 == -1)
                {
                    throw new Exception("Error: JSMIN Unterminated comment.\n");
                }
                if ((num3 == 0x2a) && (this.peek() == 0x2f))
                {
                    this.get();
                    return 0x20;
                }
            }
        }

        private int peek()
        {
            this.theLookahead = this.get();
            return this.theLookahead;
        }

        private void put(int c)
        {
            this.tw.Write((char) c);
        }
    }
}

