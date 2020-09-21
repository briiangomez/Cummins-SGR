namespace CMM.Web.Mvc
{
    using System;

    public class UrlParameter<T> where T: struct
    {
        public static T Empty
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

