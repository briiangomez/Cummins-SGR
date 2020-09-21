namespace CMM.Web.Css
{
    using System;

    public class AtRule : Statement
    {
        public const char EndToken = ';';
        public const char StartToken = '@';

        public static AtRule Parse(string str)
        {
            return new AtRule();
        }
    }
}

