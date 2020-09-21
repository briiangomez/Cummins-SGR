namespace CMM.Web.Mvc.WebResourceLoader
{
    using System;
    using System.Collections;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;

    public class CSSMinify
    {
        public static Hashtable shortColorNames = new Hashtable();
        public static Hashtable shortHexColors = new Hashtable();

        static CSSMinify()
        {
            createHashTable();
        }

        private static string BreakLines(string css, int columnWidth)
        {
            int index = 0;
            int num2 = 0;
            StringBuilder builder = new StringBuilder(css);
            while (index < builder.Length)
            {
                char ch = builder[index++];
                if ((ch == '}') && ((index - num2) > columnWidth))
                {
                    builder.Insert(index, '\n');
                    num2 = index;
                }
            }
            return builder.ToString();
        }

        private static void createHashTable()
        {
            shortColorNames.Add("F0FFFF".ToLower(), "Azure".ToLower());
            shortColorNames.Add("F5F5DC".ToLower(), "Beige".ToLower());
            shortColorNames.Add("FFE4C4".ToLower(), "Bisque".ToLower());
            shortColorNames.Add("A52A2A".ToLower(), "Brown".ToLower());
            shortColorNames.Add("FF7F50".ToLower(), "Coral".ToLower());
            shortColorNames.Add("FFD700".ToLower(), "Gold".ToLower());
            shortColorNames.Add("808080".ToLower(), "Grey".ToLower());
            shortColorNames.Add("008000".ToLower(), "Green".ToLower());
            shortColorNames.Add("4B0082".ToLower(), "Indigo".ToLower());
            shortColorNames.Add("FFFFF0".ToLower(), "Ivory".ToLower());
            shortColorNames.Add("F0E68C".ToLower(), "Khaki".ToLower());
            shortColorNames.Add("FAF0E6".ToLower(), "Linen".ToLower());
            shortColorNames.Add("800000".ToLower(), "Maroon".ToLower());
            shortColorNames.Add("000080".ToLower(), "Navy".ToLower());
            shortColorNames.Add("808000".ToLower(), "Olive".ToLower());
            shortColorNames.Add("FFA500".ToLower(), "Orange".ToLower());
            shortColorNames.Add("DA70D6".ToLower(), "Orchid".ToLower());
            shortColorNames.Add("CD853F".ToLower(), "Peru".ToLower());
            shortColorNames.Add("FFC0CB".ToLower(), "Pink".ToLower());
            shortColorNames.Add("DDA0DD".ToLower(), "Plum".ToLower());
            shortColorNames.Add("800080".ToLower(), "Purple".ToLower());
            shortColorNames.Add("FA8072".ToLower(), "Salmon".ToLower());
            shortColorNames.Add("A0522D".ToLower(), "Sienna".ToLower());
            shortColorNames.Add("C0C0C0".ToLower(), "Silver".ToLower());
            shortColorNames.Add("FFFAFA".ToLower(), "Snow".ToLower());
            shortColorNames.Add("D2B48C".ToLower(), "Tan".ToLower());
            shortColorNames.Add("008080".ToLower(), "Teal".ToLower());
            shortColorNames.Add("FF6347".ToLower(), "Tomato".ToLower());
            shortColorNames.Add("EE82EE".ToLower(), "Violet".ToLower());
            shortColorNames.Add("F5DEB3".ToLower(), "Wheat".ToLower());
            shortHexColors.Add("black", "#000");
            shortHexColors.Add("fuchsia", "#f0f");
            shortHexColors.Add("lightSlategray", "#789");
            shortHexColors.Add("lightSlategrey", "#789");
            shortHexColors.Add("magenta", "#f0f");
            shortHexColors.Add("white", "#fff");
            shortHexColors.Add("yellow", "#ff0");
        }

        private static void HasAllBackGroundProperties(Regex re, ref string CSSText)
        {
            MatchCollection mcProperySet = re.Matches(CSSText);
            int num = 5;
            if (mcProperySet.Count == num)
            {
                int num2 = 0;
                for (int i = 0; i < num; i++)
                {
                    string str = mcProperySet[i].Groups["property"].Value;
                    if (str != null)
                    {
                        if (!(str == "color"))
                        {
                            if (str == "image")
                            {
                                goto Label_009C;
                            }
                            if (str == "repeat")
                            {
                                goto Label_00A2;
                            }
                            if (str == "attachment")
                            {
                                goto Label_00A8;
                            }
                            if (str == "position")
                            {
                                goto Label_00AE;
                            }
                        }
                        else
                        {
                            num2++;
                        }
                    }
                    continue;
                Label_009C:
                    num2 += 2;
                    continue;
                Label_00A2:
                    num2 += 4;
                    continue;
                Label_00A8:
                    num2 += 8;
                    continue;
                Label_00AE:
                    num2 += 0x10;
                }
                if (num2 == 0x1f)
                {
                    CSSText = ShortHandBackGroundReplaceV2(mcProperySet, re, CSSText);
                }
            }
        }

        private static void HasAllFontProperties(Regex re, ref string CSSText)
        {
            MatchCollection mcProperySet = re.Matches(CSSText);
            int num = 5;
            if (mcProperySet.Count == num)
            {
                int num2 = 0;
                for (int i = 0; i < num; i++)
                {
                    string str = mcProperySet[i].Groups["fontProperty"].Value;
                    if (str != null)
                    {
                        if (!(str == "style"))
                        {
                            if (str == "variant")
                            {
                                goto Label_009C;
                            }
                            if (str == "weight")
                            {
                                goto Label_00A2;
                            }
                            if (str == "size")
                            {
                                goto Label_00A8;
                            }
                            if (str == "family")
                            {
                                goto Label_00AE;
                            }
                        }
                        else
                        {
                            num2++;
                        }
                    }
                    continue;
                Label_009C:
                    num2 += 2;
                    continue;
                Label_00A2:
                    num2 += 4;
                    continue;
                Label_00A8:
                    num2 += 8;
                    continue;
                Label_00AE:
                    num2 += 0x10;
                }
                if (num2 == 0x1f)
                {
                    CSSText = ShortHandFontReplaceV2(mcProperySet, re, CSSText);
                }
            }
        }

        private static void HasAllListStyle(Regex re, ref string CSSText)
        {
            int num = 3;
            MatchCollection mcProperySet = re.Matches(CSSText);
            if (mcProperySet.Count == num)
            {
                int num2 = 0;
                for (int i = 0; i < num; i++)
                {
                    string str = mcProperySet[i].Groups["style"].Value;
                    if (str != null)
                    {
                        if (!(str == "type"))
                        {
                            if (str == "image")
                            {
                                goto Label_007D;
                            }
                            if (str == "position")
                            {
                                goto Label_0083;
                            }
                        }
                        else
                        {
                            num2++;
                        }
                    }
                    continue;
                Label_007D:
                    num2 += 2;
                    continue;
                Label_0083:
                    num2 += 4;
                }
                if (num2 == 7)
                {
                    CSSText = ShortHandListReplaceV2(mcProperySet, re, CSSText);
                }
            }
        }

        private static void HasAllPositions(Regex re, ref string CSSText)
        {
            MatchCollection mcProperySet = re.Matches(CSSText);
            if (mcProperySet.Count == 4)
            {
                int num = 0;
                for (int i = 0; i < 4; i++)
                {
                    string str = mcProperySet[i].Groups["position"].Value;
                    if (str != null)
                    {
                        if (!(str == "top"))
                        {
                            if (str == "right")
                            {
                                goto Label_0087;
                            }
                            if (str == "bottom")
                            {
                                goto Label_008D;
                            }
                            if (str == "left")
                            {
                                goto Label_0093;
                            }
                        }
                        else
                        {
                            num++;
                        }
                    }
                    continue;
                Label_0087:
                    num += 2;
                    continue;
                Label_008D:
                    num += 4;
                    continue;
                Label_0093:
                    num += 8;
                }
                if (num == 15)
                {
                    CSSText = ShortHandReplaceV2(mcProperySet, re, CSSText);
                }
            }
        }

        public static string Minify(UrlHelper urlHelper, string cssPath, string requestPath, string cssContent)
        {
            MatchEvaluator urlReplacer = delegate (Match m) {
                string uriString = m.Value;
                Uri uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
                if (uri.IsAbsoluteUri || VirtualPathUtility.IsAbsolute(uriString))
                {
                    return uriString;
                }
                uriString = VirtualPathUtility.Combine(cssPath, uriString);
                uriString = VirtualPathUtility.MakeRelative(requestPath, uriString);
                return urlHelper.Content(uriString);
            };
            return Minify(urlHelper, urlReplacer, cssContent, 0);
        }

        public static string Minify(UrlHelper urlHelper, MatchEvaluator urlReplacer, string cssContent, int columnWidth)
        {
            MatchEvaluator evaluator = new MatchEvaluator(CSSMinify.RGBMatchHandler);
            MatchEvaluator evaluator2 = new MatchEvaluator(CSSMinify.ShortColorNameMatchHandler);
            MatchEvaluator evaluator3 = new MatchEvaluator(CSSMinify.ShortColorHexMatchHandler);
            cssContent = RemoveCommentBlocks(cssContent);
            cssContent = Regex.Replace(cssContent, @"\s+", " ");
            cssContent = Regex.Replace(cssContent, @"\x22\x5C\x22}\x5C\\x22\x22", "___PSEUDOCLASSBMH___");
            cssContent = Regex.Replace(cssContent, @"(?#no preceding space needed)\s+((?:[!{};>+()\],])|(?<={[^{}]*):(?=[^}]*}))", "$1");
            cssContent = Regex.Replace(cssContent, @"([!{}:;>+([,])\s+", "$1");
            cssContent = Regex.Replace(cssContent, "([^;}])}", "$1;}");
            cssContent = Regex.Replace(cssContent, @"(\d+)\.0+(p(?:[xct])|(?:[cem])m|%|in|ex)\b", "$1$2");
            cssContent = Regex.Replace(cssContent, @"([\s:])(0)(px|em|%|in|cm|mm|pc|pt|ex)\b", "$1$2");
            cssContent = Regex.Replace(cssContent, @"(?<=font-weight:)normal\b", "400");
            cssContent = Regex.Replace(cssContent, @"(?<=font-weight:)bold\b", "700");
            cssContent = ShortHandAllProperties(cssContent);
            cssContent = Regex.Replace(cssContent, @":\s*((inherit|auto|0|(?:(?:\d*\.?\d+(?:p(?:[xct])|(?:[cem])m|%|in|ex))))\s+(inherit|auto|0|(?:(?:\d?\.?\d(?:p(?:[xct])|(?:[cem])m|%|in|ex)))))\s+\2\s+\3;", ":$1;", RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, @":\s*((?:(?:inherit|auto|0|(?:(?:\d*\.?\d+(?:p(?:[xct])|(?:[cem])m|%|in|ex))))\s+)?(inherit|auto|0|(?:(?:\d?\.?\d(?:p(?:[xct])|(?:[cem])m|%|in|ex))))\s+(?:0|(?:(?:\d?\.?\d(?:p(?:[xct])|(?:[cem])m|%|in|ex)))))\s+\2;", ":$1;", RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, "background-position:0;", "background-position:0 0;");
            cssContent = Regex.Replace(cssContent, @"(:|\s)0+\.(\d+)", "$1.$2");
            cssContent = Regex.Replace(cssContent, @"(outline|border)-style\s*:\s*(none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset)(?:\s+\2){1,3};", "$1-style:$2;", RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, @"(outline|border)-style\s*:\s*((none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset)\s+(none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset ))(?:\s+\3)(?:\s+\4);", "$1-style:$2;", RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, @"(outline|border)-style\s*:\s*((?:(?:none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset)\s+)?(none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset )\s+(?:none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset ))(?:\s+\3);", "$1-style:$2;", RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, @"(outline|border)-style\s*:\s*((none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset)\s+(?:none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset ))(?:\s+\3);", "$1-style:$2;", RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, @"(outline|border)-color\s*:\s*((?:\#(?:[0-9A-F]{3}){1,2})|\S+)(?:\s+\2){1,3};", "$1-color:$2;", RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, @"(outline|border)-color\s*:\s*(((?:\#(?:[0-9A-F]{3}){1,2})|\S+)\s+((?:\#(?:[0-9A-F]{3}){1,2})|\S+))(?:\s+\3)(?:\s+\4);", "$1-color:$2;", RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, @"(outline|border)-color\s*:\s*((?:(?:(?:\#(?:[0-9A-F]{3}){1,2})|\S+)\s+)?((?:\#(?:[0-9A-F]{3}){1,2})|\S+)\s+(?:(?:\#(?:[0-9A-F]{3}){1,2})|\S+))(?:\s+\3);", "$1-color:$2;", RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, @"rgb\s*\x28((?:25[0-5])|(?:2[0-4]\d)|(?:[01]?\d?\d))\s*,\s*((?:25[0-5])|(?:2[0-4]\d)|(?:[01]?\d?\d))\s*,\s*((?:25[0-5])|(?:2[0-4]\d)|(?:[01]?\d?\d))\s*\x29", evaluator);
            cssContent = Regex.Replace(cssContent, @"(?<![\x22\x27=]\s*)\#(?:([0-9A-F])\1)(?:([0-9A-F])\2)(?:([0-9A-F])\3)", "#$1$2$3", RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, @"(?<=color\s*:\s*.*)\#(?<hex>f00)\b", "red", RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, @"(?<=color\s*:\s*.*)\#(?<hex>[0-9a-f]{6})", evaluator2, RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, @"(?<=color\s*:\s*)\b(Black|Fuchsia|LightSlateGr[ae]y|Magenta|White|Yellow)\b", evaluator3, RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, "(?<=url\\(\\s*([\"']?))(?<url>[^'\"]+?)(?=\\1\\s*\\))", urlReplacer, RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, "(?<=@import\\s*([\"']))(?<url>[^'\"]+?)(?=\\1\\s*;)", urlReplacer, RegexOptions.IgnoreCase);
            cssContent = Regex.Replace(cssContent, "[^}]+{;}", "");
            cssContent = Regex.Replace(cssContent, ";(})", "$1");
            if (columnWidth > 0)
            {
                cssContent = BreakLines(cssContent, columnWidth);
            }
            return cssContent;
        }

        private static string RemoveCommentBlocks(string input)
        {
            int startIndex = 0;
            int index = 0;
            bool flag = false;
            for (startIndex = input.IndexOf("/*", startIndex); startIndex >= 0; startIndex = input.IndexOf("/*", startIndex))
            {
                index = input.IndexOf("*/", (int) (startIndex + 2));
                if (index >= (startIndex + 2))
                {
                    if (input[index - 1] == '\\')
                    {
                        startIndex = index + 2;
                        flag = true;
                    }
                    else if (flag)
                    {
                        startIndex = index + 2;
                        flag = false;
                    }
                    else
                    {
                        input = input.Remove(startIndex, (index + 2) - startIndex);
                    }
                }
            }
            return input;
        }

        private static string ReplaceNonEmpty(string inputText, string replacementText)
        {
            if (replacementText.Trim() != string.Empty)
            {
                inputText = string.Format(" {0}", replacementText);
            }
            return inputText;
        }

        private static string RGBMatchHandler(Match m)
        {
            StringBuilder builder = new StringBuilder("#");
            for (int i = 1; i <= 3; i++)
            {
                builder.Append(int.Parse(m.Groups[i].Value).ToString("x2"));
            }
            return builder.ToString();
        }

        private static string ShortColorHexMatchHandler(Match m)
        {
            return shortHexColors[m.Value.ToString().ToLower()].ToString();
        }

        private static string ShortColorNameMatchHandler(Match m)
        {
            string str = m.Value;
            if (shortColorNames.ContainsKey(m.Groups["hex"].Value))
            {
                str = shortColorNames[m.Groups["hex"].Value].ToString();
            }
            return str;
        }

        private static string ShortHandAllProperties(string css)
        {
            Regex regex = new Regex("{[^{}]*}");
            Regex re = new Regex(@"(?<fullProperty>(?:(?<property>padding)-(?<position>top|right|bottom|left)))\s*:\s*(?<unit>[\w.]+);?", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@"(?<fullProperty>(?:(?<property>margin)-(?<position>top|right|bottom|left)))\s*:\s*(?<unit>[\w.]+);?", RegexOptions.IgnoreCase);
            Regex regex4 = new Regex(@"(?<fullProperty>(?<property>border)-(?<position>top|right|bottom|left)(?<property2>-(?:color)))\s*:\s*(?<unit>[#\w.]+);?", RegexOptions.IgnoreCase);
            Regex regex5 = new Regex(@"(?<fullProperty>(?<property>border)-(?<position>top|right|bottom|left)(?<property2>-(?:style)))\s*:\s*(?<unit>none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset);?", RegexOptions.IgnoreCase);
            Regex regex6 = new Regex(@"(?<fullProperty>(?<property>border)-(?<position>top|right|bottom|left)(?<property2>-(?:width)))\s*:\s*(?<unit>[\w.]+);?", RegexOptions.IgnoreCase);
            Regex regex7 = new Regex(@"list-style-(?<style>type|image|position)\s*:\s*(?<unit>[^};]+);?", RegexOptions.IgnoreCase);
            Regex regex8 = new Regex("font-(?:(?:(?<fontProperty>family\\b)\\s*:\\s*(?<fontPropertyValue>(?:\\b[a-zA-Z]+(-[a-zA-Z]+)?\\b|\\x22[^\\x22]+\\x22)(?:\\s*,\\s*(?:\\b[a-zA-Z]+(-[a-zA-Z]+)?\\b|\\x22[^\\x22]+\\x22))*)\\b)|\r\n(?:(?<fontProperty>style\\b)\\s*:\\s*(?<fontPropertyValue>normal|italic|oblique|inherit))|\r\n(?:(?<fontProperty>variant\\b)\\s*:\\s*(?<fontPropertyValue>normal|small-caps|inherit))|\r\n(?:(?<fontProperty>weight\\b)\\s*:\\s*(?<fontPropertyValue>normal|bold|(?:bold|light)er|[1-9]00|inherit))|\r\n(?:(?<fontProperty>size\\b)\\s*:\\s*(?<fontPropertyValue>(?:(?:xx?-)?(?:small|large))|medium|(?:\\d*\\.?\\d+(?:%|(p(?:[xct])|(?:[cem])m|in|ex))\\b)|inherit|\\b0\\b)))\\s*;?", RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
            Regex regex9 = new Regex("background-(?:\r\n(?:(?<property>color)\\s*:\\s*(?<unit>transparent|inherit|(?:(?:\\#(?:[0-9A-F]{3}){1,2})|\\S+)))|\r\n(?:(?<property>image)\\s*:\\s*(?<unit>none|inherit|(?:url\\s*\\([^()]+\\))))|\r\n(?:(?<property>repeat)\\s*:\\s*(?<unit>no-repeat|inherit|repeat(?:-[xy])))|\r\n(?:(?<property>attachment)\\s*:\\s*(?<unit>scroll|inherit|fixed))|\r\n(?:(?<property>position)\\s*:\\s*(?<unit>((?<horizontal>left | center | right|(?:0|(?:(?:\\d*\\.?\\d+(?:p(?:[xct])|(?:[cem])m|%|in|ex)))))\\s+(?<vertical>top | center | bottom |(?:0|(?:(?:\\d*\\.?\\d+(?:p(?:[xct])|(?:[cem])m|%|in|ex))))))|\r\n    ((?<vertical>top | center | bottom )\\s+(?<horizontal>left | center | right ))|\r\n    ((?<horizontal>left | center | right )|(?<vertical>top | center | bottom ))))\r\n);?", RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            MatchCollection matchs = regex.Matches(css);
            foreach (Match match in matchs)
            {
                string cSSText = match.Value;
                HasAllPositions(re, ref cSSText);
                HasAllPositions(regex3, ref cSSText);
                HasAllPositions(regex4, ref cSSText);
                HasAllPositions(regex5, ref cSSText);
                HasAllPositions(regex6, ref cSSText);
                HasAllListStyle(regex7, ref cSSText);
                HasAllFontProperties(regex8, ref cSSText);
                HasAllBackGroundProperties(regex9, ref cSSText);
                css = css.Replace(match.Value, cSSText);
            }
            return css;
        }

        private static string ShortHandBackGroundReplaceV2(MatchCollection mcProperySet, Regex re, string InputText)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            foreach (Match match in mcProperySet)
            {
                string str10 = match.Groups["property"].Value;
                if (str10 != null)
                {
                    if (!(str10 == "color"))
                    {
                        if (str10 == "image")
                        {
                            goto Label_0104;
                        }
                        if (str10 == "repeat")
                        {
                            goto Label_0155;
                        }
                        if (str10 == "attachment")
                        {
                            goto Label_01A6;
                        }
                        if (str10 == "position")
                        {
                            goto Label_01F4;
                        }
                    }
                    else if (match.Groups["unit"].Value != "transparent")
                    {
                        str = string.Format(" {0}", match.Groups["unit"].Value);
                    }
                }
                goto Label_0243;
            Label_0104:
                if (match.Groups["unit"].Value != "none")
                {
                    str2 = string.Format(" {0}", match.Groups["unit"].Value);
                }
                goto Label_0243;
            Label_0155:
                if (match.Groups["unit"].Value != "repeat")
                {
                    str3 = string.Format(" {0}", match.Groups["unit"].Value);
                }
                goto Label_0243;
            Label_01A6:
                if (match.Groups["unit"].Value != "scroll")
                {
                    str4 = string.Format(" {0}", match.Groups["unit"].Value);
                }
                goto Label_0243;
            Label_01F4:
                if (match.Groups["unit"].Value != "0% 0%")
                {
                    str5 = string.Format(" {0}", match.Groups["unit"].Value);
                }
            Label_0243:;
            }
            string str6 = string.Format("background:{0}", string.Format("{0}{1}{2}{3}{4};", new object[] { str, str2, str3, str4, str5 }).Trim());
            return re.Replace(InputText, "").Insert(1, str6);
        }

        private static string ShortHandFontReplaceV2(MatchCollection mcProperySet, Regex re, string InputText)
        {
            Regex regex = new Regex(@"line-height\s*:\s*((?:\d*\.?\d+(?:%|(p(?:[xct])|(?:[cem])m|in|ex)\b)?)|normal|inherit);?", RegexOptions.IgnoreCase);
            string str = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            string str6 = string.Empty;
            foreach (Match match in mcProperySet)
            {
                string str11 = match.Groups[""].Value;
                if (str11 != null)
                {
                    if (!(str11 == "family"))
                    {
                        if (str11 == "size")
                        {
                            goto Label_00EA;
                        }
                        if (((str11 == "style") || (str11 == "variant")) || (str11 == "weight"))
                        {
                            goto Label_019F;
                        }
                    }
                    else
                    {
                        str = string.Format(" {0}", match.Groups["fontPropertyValue"].Value);
                    }
                }
                goto Label_01F5;
            Label_00EA:
                if (regex.IsMatch(InputText))
                {
                    Match match2 = regex.Match(InputText);
                    if (match2.Groups[1].Value != "normal")
                    {
                        str5 = string.Format("/{0}", match2.Groups[1].Value);
                    }
                    InputText = regex.Replace(InputText, string.Empty);
                }
                str5 = string.Format(" {0}{1}", match.Groups["fontPropertyValue"].Value, str5);
                if (str5 == "medium")
                {
                    str5 = string.Empty;
                }
                goto Label_01F5;
            Label_019F:
                if (match.Groups["fontPropertyValue"].Value != "normal")
                {
                    str6 = str6 + string.Format(" {0}", match.Groups["fontPropertyValue"].Value);
                }
            Label_01F5:;
            }
            string str7 = string.Format("font:{0}", string.Format("{0}{1}{2};", new object[] { str6, str3, str4, str5, str }).Trim());
            return re.Replace(InputText, "").Insert(1, str7);
        }

        private static string ShortHandListReplaceV2(MatchCollection mcProperySet, Regex re, string InputText)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            foreach (Match match in mcProperySet)
            {
                string str7 = match.Groups["style"].Value;
                if (str7 != null)
                {
                    if (!(str7 == "type"))
                    {
                        if (str7 == "position")
                        {
                            goto Label_00C4;
                        }
                        if (str7 == "style")
                        {
                            goto Label_0110;
                        }
                    }
                    else if (match.Groups["unit"].Value != "disc")
                    {
                        str = match.Groups["unit"].Value;
                    }
                }
                goto Label_015C;
            Label_00C4:
                if (match.Groups["unit"].Value != "outside")
                {
                    str2 = string.Format(" {0}", match.Groups["unit"].Value);
                }
                goto Label_015C;
            Label_0110:
                if (match.Groups["unit"].Value != "none")
                {
                    str3 = string.Format(" {0}", match.Groups["unit"].Value);
                }
            Label_015C:;
            }
            string str4 = string.Format("list-style:{0}{1}{2};", str, str2, str3);
            return re.Replace(InputText, "").Insert(1, str4);
        }

        private static string ShortHandReplaceV2(MatchCollection mcProperySet, Regex reTRBL1, string InputText)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Format("{0}{1}", mcProperySet[0].Groups["property"].Value, mcProperySet[0].Groups["property2"].Value);
            foreach (Match match in mcProperySet)
            {
                string str9 = match.Groups["position"].Value;
                if (str9 != null)
                {
                    if (!(str9 == "top"))
                    {
                        if (str9 == "right")
                        {
                            goto Label_00EA;
                        }
                        if (str9 == "bottom")
                        {
                            goto Label_0103;
                        }
                        if (str9 == "left")
                        {
                            goto Label_011C;
                        }
                    }
                    else
                    {
                        str = match.Groups["unit"].Value;
                    }
                }
                goto Label_0135;
            Label_00EA:
                str2 = match.Groups["unit"].Value;
                goto Label_0135;
            Label_0103:
                str3 = match.Groups["unit"].Value;
                goto Label_0135;
            Label_011C:
                str4 = match.Groups["unit"].Value;
            Label_0135:;
            }
            string str6 = string.Format("{0}:{1} {2} {3} {4};", new object[] { str5, str, str2, str3, str4 });
            return reTRBL1.Replace(InputText, "").Insert(1, str6);
        }
    }
}

