using Microsoft.AspNetCore.Authorization;
using System;

namespace SGR.Communicator
{
    public class AccessFilterAttribute : AuthorizeAttribute
    {
        public AccessFilterAttribute(string function)
        {
            if (string.IsNullOrEmpty(function))
            {
                throw new ArgumentNullException("function");
            }
            this.Function = function;
        }

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    return (CommunicatorSite.FullMenusDictionary.ContainsKey(this.Function) && (CommunicatorContext.Current.User.IsAdmin || CommunicatorContext.Current.AccessableMenus.Contains(this.Function)));
        //}

        public string Function { get; private set; }
    }
}

