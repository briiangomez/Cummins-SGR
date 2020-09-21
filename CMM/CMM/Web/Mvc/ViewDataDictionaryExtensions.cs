namespace CMM.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public static class ViewDataDictionaryExtensions
    {
        public static ViewDataDictionary Merge(this ViewDataDictionary source, ViewDataDictionary dic1)
        {
            if (dic1 != null)
            {
                foreach (KeyValuePair<string, object> pair in dic1)
                {
                    if (!source.ContainsKey(pair.Key))
                    {
                        source.Add(pair.Key, pair.Value);
                    }
                }
                foreach (KeyValuePair<string, ModelState> pair2 in dic1.ModelState)
                {
                    if (!source.ModelState.ContainsKey(pair2.Key))
                    {
                        source.ModelState.Add(pair2.Key, pair2.Value);
                    }
                }
                source.Model = dic1.Model;
                source.TemplateInfo = dic1.TemplateInfo;
                source.ModelMetadata = dic1.ModelMetadata;
            }
            return source;
        }
    }
}

