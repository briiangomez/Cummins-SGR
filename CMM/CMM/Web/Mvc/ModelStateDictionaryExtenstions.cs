namespace CMM.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public static class ModelStateDictionaryExtenstions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary modelStates)
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, ModelState> pair in modelStates)
            {
                if (pair.Value.Errors.Count > 0)
                {
                    foreach (ModelError error in pair.Value.Errors)
                    {
                        list.Add(pair.Key + ":" + error.ErrorMessage);
                    }
                }
            }
            return list;
        }
    }
}

