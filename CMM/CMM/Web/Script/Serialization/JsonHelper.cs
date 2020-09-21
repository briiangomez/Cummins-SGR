namespace CMM.Web.Script.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Compilation;
    using System.Web.Configuration;
    using System.Web.Script.Serialization;

    public static class JsonHelper
    {
        private static readonly long DatetimeMinTimeTicks;
        private static JavaScriptSerializer jsonSerializer;

        static JsonHelper()
        {
            DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DatetimeMinTimeTicks = time.Ticks;
            jsonSerializer = new JavaScriptSerializer();
            ScriptingJsonSerializationSection section = (ScriptingJsonSerializationSection) ConfigurationManager.GetSection("system.web.extensions/scripting/webServices/jsonSerialization");
            jsonSerializer.MaxJsonLength = section.MaxJsonLength;
            jsonSerializer.RecursionLimit = section.RecursionLimit;
            jsonSerializer.RegisterConverters(CreateConverters(section.Converters));
        }

        private static void AppendCharAsUnicode(StringBuilder builder, char c)
        {
            builder.Append(@"\u");
            builder.AppendFormat(CultureInfo.InvariantCulture, "{0:x4}", new object[] { (int) c });
        }

        internal static JavaScriptConverter[] CreateConverters(ConvertersCollection converters)
        {
            List<JavaScriptConverter> list = new List<JavaScriptConverter>();
            foreach (Converter converter in converters)
            {
                Type type = BuildManager.GetType(converter.Type, false);
                list.Add((JavaScriptConverter) Activator.CreateInstance(type));
            }
            return list.ToArray();
        }

        public static T Deserialize<T>(string json)
        {
            return jsonSerializer.Deserialize<T>(json);
        }

        public static DateTime JsonStringToDateTime(string s)
        {
            long num;
            if (long.TryParse(Regex.Match(s, @"^/Date\((?<ticks>-?[0-9]+)(?:[a-zA-Z]|(?:\+|-)[0-9]{4})?\)/").Groups["ticks"].Value, out num))
            {
                return new DateTime((num * 0x2710L) + DatetimeMinTimeTicks, DateTimeKind.Utc);
            }
            return DateTime.MinValue;
        }

        public static string QuoteString(string value)
        {
            StringBuilder builder = null;
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            int startIndex = 0;
            int count = 0;
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if ((((c == '\r') || (c == '\t')) || ((c == '"') || (c == '\''))) || ((((c == '<') || (c == '>')) || ((c == '\\') || (c == '\n'))) || (((c == '\b') || (c == '\f')) || (c < ' '))))
                {
                    if (builder == null)
                    {
                        builder = new StringBuilder(value.Length + 5);
                    }
                    if (count > 0)
                    {
                        builder.Append(value, startIndex, count);
                    }
                    startIndex = i + 1;
                    count = 0;
                }
                switch (c)
                {
                    case '<':
                    case '>':
                    case '\'':
                    {
                        AppendCharAsUnicode(builder, c);
                        continue;
                    }
                    case '\\':
                    {
                        builder.Append(@"\\");
                        continue;
                    }
                    case '\b':
                    {
                        builder.Append(@"\b");
                        continue;
                    }
                    case '\t':
                    {
                        builder.Append(@"\t");
                        continue;
                    }
                    case '\n':
                    {
                        builder.Append(@"\n");
                        continue;
                    }
                    case '\f':
                    {
                        builder.Append(@"\f");
                        continue;
                    }
                    case '\r':
                    {
                        builder.Append(@"\r");
                        continue;
                    }
                    case '"':
                    {
                        builder.Append("\\\"");
                        continue;
                    }
                }
                if (c < ' ')
                {
                    AppendCharAsUnicode(builder, c);
                }
                else
                {
                    count++;
                }
            }
            if (builder == null)
            {
                return value;
            }
            if (count > 0)
            {
                builder.Append(value, startIndex, count);
            }
            return builder.ToString();
        }

        public static string QuoteString(string value, bool addQuotes)
        {
            string str = QuoteString(value);
            if (addQuotes)
            {
                str = "\"" + str + "\"";
            }
            return str;
        }

        public static void RegisterConverter(JavaScriptConverter converter)
        {
            jsonSerializer.RegisterConverters(new JavaScriptConverter[] { converter });
        }

        public static string ToJSON(this object obj)
        {
            return jsonSerializer.Serialize(obj);
        }
    }
}

