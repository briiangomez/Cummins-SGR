namespace CMM.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public static class DictionaryExtensions
    {
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> dic1)
        {
            if (dic1 != null)
            {
                foreach (KeyValuePair<TKey, TValue> pair in dic1)
                {
                    if (!source.ContainsKey(pair.Key))
                    {
                        source.Add(pair);
                    }
                }
            }
            return source;
        }

        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            dic[key] = value;
            return dic;
        }

        public static NameValueCollection ToNameValueCollection<TKey, TValue>(this IDictionary<TKey, TValue> dic)
        {
            NameValueCollection values = new NameValueCollection();
            foreach (TKey local in dic.Keys)
            {
                values[local.ToString()] = (dic[local] == null) ? "" : dic[local].ToString();
            }
            return values;
        }
    }
}

