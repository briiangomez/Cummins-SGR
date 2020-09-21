namespace CMM.Web.Css
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    public class StyleSheet
    {
        private List<Statement> _statements;

        public static StyleSheet Load(StyleSheetReader reader)
        {
            StyleSheet sheet = new StyleSheet();
            while (!reader.EndOfStream)
            {
                switch (reader.Status)
                {
                    case StyleSheetReader.ReadStatus.AtRule:
                        sheet.Statements.Add(AtRule.Parse(reader.ReadAtRule()));
                        break;

                    case StyleSheetReader.ReadStatus.RuleSet:
                        sheet.Statements.Add(RuleSet.Parse(reader.ReadRuleSet()));
                        break;
                }
            }
            return sheet;
        }

        public static StyleSheet Load(Stream stream)
        {
            return Load(new StyleSheetReader(stream));
        }

        public static StyleSheet Load(TextReader reader)
        {
            return Load(new StyleSheetReader(reader));
        }

        public static StyleSheet Load(string str)
        {
            return Load(new StyleSheetReader(Regex.Replace(str, @"(/\*[\w\W]*?\*/)|(<--)|(-->)", string.Empty)));
        }

        public IList<Statement> Statements
        {
            get
            {
                if (this._statements == null)
                {
                    this._statements = new List<Statement>();
                }
                return this._statements;
            }
        }
    }
}

