using System;

namespace SGR.Communicator
{
    public class AdminAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public AdminAttribute(string function)
        {
            if (string.IsNullOrEmpty(function))
            {
                throw new ArgumentNullException("function");
            }
            this.Function = function;
        }

        //protected override bool AuthorizeCore()
        //{
        //    return (CommunicatorSite.FullMenusDictionary.ContainsKey(this.Function));// && CommunicatorContext.Current.User.IsAdmin);
        //}

        public string Function { get; private set; }
    }
}

